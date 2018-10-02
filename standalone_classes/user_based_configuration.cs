/*
  With a little bit of simplification this bit of code has some real potential - I'll 
  see what I can do with it to make life easier... the more I can shove off on the user to configure,
  then the faster I can roll out code....... Oh wait that's how we ended up with the dct mess.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

public class user_based_configurations
{
    /*
     * Need to simplify some of these processes, make them a little bit more modular.
     */
    //  private string user_to_use { get; set; } // we'll store user based configuration and operate in 
    //  private string system_name { get; set; }
    // I could probably just store the key prefix and not the components.
    private string key_prefix { get; set; }
    private List<Tuple<string,string>> configs_set { get; set; }
    private Dictionary<string,string> setting_set { get; set;  } // wrapping the configuration names up in here...
    private Application application { get; set; }
    // whatever the local manager is will dictate what keys are generated for the users...


    public user_based_configurations(List<Tuple<string, string>> configuration_set)//, Application application)
    {
        //this.user_to_use = Environment.UserName;
        //this.system_name = Environment.MachineName;
        this.key_prefix = Environment.UserDomainName + "-" + Environment.UserName; // that should be adequate for each of the configuration options.
        this.configs_set = configuration_set;
        this.update_configs(configuration_set); // simplifies updates.
        this.application = application;
    }

    #region private methods
    // we'll be using the local user to truncate.
    private void clear_user_settings()
    {
        // iterate through the keys that the user controls and then rewrite them?
        foreach(string a in user_keys())
        {
            ConfigurationManager.AppSettings.Remove(a);
        }
    }

    // this simply truncates the values as "" --> need to set up an override that would update it.
    private void reset_user_settings()
    {
        foreach(string a in user_keys())
        {
            ConfigurationManager.AppSettings.Remove(a);
            ConfigurationManager.AppSettings.Set(a, "");
        }
    }

    #endregion

    #region inheritable methods
    //
    #endregion

    #region public methods
    public List<string> user_keys()
    {
        List<string> keys = ConfigurationManager.AppSettings.AllKeys.ToList();
        List<string> real_keys = new List<string>();
        foreach( string a in keys)
        {
            if (a.Contains(key_prefix))
            {
                real_keys.Add(a);
            }
        }
        return real_keys;
    }

    public List<string> all_keys()
    {
        return ConfigurationManager.AppSettings.AllKeys.ToList<string>();
    }

    public string get_setting(string key)
    {
        return ConfigurationManager.AppSettings.Get(key);
    }

    /// <summary>
    /// use this to get default user settings.
    /// I'm not sure what I'm going to set up as configurations - but it'll be a 
    /// rolling set of updates.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string get_user_setting(string key)
    {
        return ConfigurationManager.AppSettings.Get(key_prefix + "_" + key);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration_set"></param>
    /// <returns>number of configs added by the method - we'll be using this instead of the constructor.</returns>
    public int update_configs(List<Tuple<string,string>> configuration_set)
    {
        int counter = 0;
        var keyset = ConfigurationManager.AppSettings.AllKeys;
        var confsec = ConfigurationManager.GetSection("appSettings");
        var m = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Console.WriteLine(m.AppSettings.IsReadOnly());

        foreach (Tuple<string, string> a in configuration_set)
        {
            if (!this.configs_set.Contains(a))
            {
                this.configs_set.Add(a);
            }
            if (!keyset.Contains(this.key_prefix + "_"+a.Item1))
            {
                var data = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);// ConfigurationUserLevel.None);
                //var data = ConfigurationManager.AppSettings;//
                //ConfigurationSection.
                //
                //ConfigurationManager.Op
                data.AppSettings.Settings.Add(new KeyValueConfigurationElement(this.key_prefix + "_" + a.Item1, a.Item2));
                foreach(string b in data.AppSettings.Settings.AllKeys.ToList<string>())
                {
                    Console.WriteLine(a);
                    Console.WriteLine(b);
                }
                data.Save();
                //ConfigurationManager.AppSettings.Add(this.key_prefix + "_" + a.Item1, a.Item2);
                counter++;
            }
        }
        return counter;
    }

    /// <summary>
    /// convenience version of the "store_config" method, saves to user key.
    /// would almost be easier to write to the registry (super easy to do).
    /// </summary>
    /// <param name="configuration_to_store"></param>
    /// <returns></returns>
    public int store_user_config(Tuple<string,string> configuration_to_store)
    {
        var a = new Tuple<string, string>(this.key_prefix + "_" + configuration_to_store.Item1, configuration_to_store.Item2);
        return store_config(a);
    }

    /// <summary>
    /// Store a configuration - doesn't adjust for name or anything.
    /// I'll set up a helper function for "store user configuration"
    /// which should allow storage of user configuration.
    /// </summary>
    /// <param name="configuration_to_store"></param>
    /// <returns></returns>
    public int store_config(Tuple<string,string> configuration_to_store)
    {
        Configuration configs = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        if (ConfigurationManager.AppSettings.AllKeys.Contains(configuration_to_store.Item1))
        {
            //ConfigurationManager.AppSettings.Set(configuration_to_store.Item1, configuration_to_store.Item2);
            configs.AppSettings.Settings.Remove(configuration_to_store.Item1);
            configs.AppSettings.Settings.Add(configuration_to_store.Item1, configuration_to_store.Item2);
        }
        else
        {
            configs.AppSettings.Settings.Add(configuration_to_store.Item1, configuration_to_store.Item2);
        }
        configs.Save();
        return 0; // we'll set up a return value that handles standard return codes.
    }

    /// <summary>
    /// Alias setup for the user based key setting - we'll work on how to read/store the key information.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Color get_user_color_from_config(string key)
    {
        return get_color_from_config(key_prefix+"_"+ key);
    }

    /// <summary>
    /// Attempt to capture and resolve a color configuration object from a text configuration file.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Color get_color_from_config(string key)
    {
        //string value = get_setting(key); // so many internal references.
        //KnownColor z = new SystemColor(value);

        // We'll just return the color combination that will represent the ....
        string value = get_setting(key);
        int a = 0;
        int r = 0;
        int g = 0;
        int b = 0;
        try
        {

            a = int.Parse(get_setting(key + "_alpha")); // that should do it.
            r = int.Parse(get_setting(key + "_red"));
            g = int.Parse(get_setting(key + "_green"));
            b = int.Parse(get_setting(key + "_blue"));
            /*
            // IF IT'S WRITING HEX - WE'LL SIMPLIFY HERE.
            a = int.Parse(value.Substring(0, 2), System.Globalization.NumberStyles.HexNumber); // that should do it.
            r = int.Parse(value.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            g = int.Parse(value.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            b = int.Parse(value.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            */
        }
        catch(FormatException FEX)
        {
            Console.WriteLine("Incorrect Color format - we'll reset this.");
            store_user_config(new Tuple<string, string>(key + "_alpha","255"));
            store_user_config(new Tuple<string, string>(key + "_red",r.ToString()));
            store_user_config(new Tuple<string, string>(key + "_green",g.ToString()));
            store_user_config(new Tuple<string, string>(key + "_blue",b.ToString()));
        }
        catch(ArgumentException ARE)
        {
            Console.WriteLine("You get a zero...");
        }
        catch(NullReferenceException NRE)
        {
            Console.WriteLine(NRE.ToString());
        }
        //return new Tuple<int, int, int>(0, 0, 0); // just storing the data.
        return Color.FromArgb(r, g, b);
    }
    #endregion
}