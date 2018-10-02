/*
  it's an implementation sample of the user_based_configurations class.
  I need to make a few tweaks to the code, but then I think it could work well in about 98% of my projects.
*/


public class Preference_Storage
{
    // I'm going to store preferences as strings in a manner that can be parsed back into the tool.
    // I think I'll store them in the executable, so I'll need to unzip the package.
    // we'll use an instance of the class to wrap the configurations-  the actual list will be stored here.
    public static List<Tuple<string,string>> config_keys { get; set; }

    public user_configurations.user_based_configurations configurations { get; set; }

    // alright it looks like we've gotten it ready for the localized configurations.
    public Preference_Storage()
    {
        config_keys = new List<Tuple<string,string>>();
        // I'm building in a conifg setup for this 
        // these are the default configurations.            
        config_keys.Add(new Tuple<string, string>("settings_height", "600"));
        config_keys.Add(new Tuple<string, string>("settings_width", "800"));
        config_keys.Add(new Tuple<string, string>("validation_interface", "classic")); // using the intermediary as a defualt - we'll see what happens when I set it up to load the specific user.
        config_keys.Add(new Tuple<string, string>("use_vinlink", "false")); // vinlink hasn't been added to this  - just remember that we need to update the construction module.
        config_keys.Add(new Tuple<string, string>("log_path", ".\\logs"));
        config_keys.Add(new Tuple<string, string>("background_color_alpha", "255"));
        config_keys.Add(new Tuple<string, string>("background_color_red", "0"));
        config_keys.Add(new Tuple<string, string>("background_color_green", "0"));
        config_keys.Add(new Tuple<string, string>("background_color_blue", "0"));
        config_keys.Add(new Tuple<string, string>("foreground_color_alpha", "255"));
        config_keys.Add(new Tuple<string, string>("foreground_color_red", "255"));
        config_keys.Add(new Tuple<string, string>("foreground_color_green", "255"));
        config_keys.Add(new Tuple<string, string>("foreground_color_blue", "255"));
        config_keys.Add(new Tuple<string, string>("allow_resize", "true"));
        config_keys.Add(new Tuple<string, string>("default_console", "false"));

        var zed = new user_configurations.user_based_configurations(config_keys); // that should add the user configs - I'll call preference storage to see what happens.

        foreach(string b in zed.all_keys())
        {

        }
        // alright - this is successfully storing the values in the executable configs file.
        // Time to see if it reads configs - 
        Console.WriteLine(zed.get_user_setting("validation_interface"));
        Console.WriteLine(zed.get_user_setting("log_path")); // default non- user log path is ""- so we should get a value if it's working.
        zed.store_user_config(new Tuple<string, string>("log_path", "C:/Local_Code/repos"));
        this.configurations = zed;
        Console.WriteLine(zed.get_user_setting("log_path"));
    }
}