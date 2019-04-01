using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ez_up
{
    public partial class Form1 : Form
    {
        string rootdir { get; set; }

        public void update_treeview(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Add("test");
        }


        // extract the dragdrop components and distill them down when I get a chance.
        public void drag_enter(object sender, DragEventArgs e)
        {

            // Following the tutorial for the drag handles.
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                Console.WriteLine("Text");
                e.Effect = DragDropEffects.Copy;
                //this.textBox1.Text 
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Console.WriteLine("File");
                e.Effect = DragDropEffects.Link;

            }
            else
            {
                // oooh boi.
                e.Effect = DragDropEffects.None; // I guess prevent unhandled drops.
            }

            /*



            // handle dropping file data?
            var f = e.Data;//.GetData("string", true);
            Console.WriteLine(f);
            //
            Console.WriteLine("DragEnter...");

            DataObject d_o = new DataObject();

            d_o.SetData(f); // painful

            this.textBox1.Text = e.Data.GetType().ToString();
            this.textBox1.Text += e;
            if (d_o.ContainsFileDropList())
            {
                Console.WriteLine("Contains File");
            }
            else if(d_o.ContainsText())
            {
                Console.WriteLine("Contains Text");
            }
            else
            {
                Console.WriteLine("Apparently contains nothing...");
            }
            this.textBox1.Text = e.Data.GetType().ToString();
            this.textBox1.Text += e;
            */
        }

        public void drop_object(object sender, DragEventArgs e)
        {
            // handle dropping file data?
            var f = e.Data;//.GetData("string", true);
            Console.WriteLine(f);
            //
            Console.WriteLine("DragDrop...");

            DataObject d_o = new DataObject();

            d_o.SetData(f); // painful

            this.textBox1.Text = e.Data.GetType().ToString();
            this.textBox1.Text += e;

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                this.textBox1.Text = e.Data.GetData(DataFormats.Text).ToString();
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //FileInfo fi = e.Data.GetData(DataFormats.FileDrop);
                var obj = e.Data.GetData(DataFormats.FileDrop); // does this work?
                Console.WriteLine(obj.GetType());

                // capture the filenames.
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop); // gotcha - that's why my cast was screwy.


                // we'll need to change the file type.
#if READ_FILES
                for (int i = 0; i < filenames.Length; i++)
                {
                    this.textBox1.Text += File.ReadAllText(filenames[i]);
                }
                this.textBox1.ScrollBars = ScrollBars.Both;
#endif
                for (int i = 0; i < filenames.Length; i++)
                {
                    this.textBox1.Text += filenames[i]; //                

                    // add in the tooling to pull the data as necessary.
                }
                // add a tool...
                // this might work, if it does I'll be happy and use this to build a proto compiler.
                try
                {
                    int sent = 0;
                    Program.ftp_files(sent,filenames,Program.configs)
                }

                //



            }
            
        }



        public Form1()
        {
            InitializeComponent();
            //
#if DEBUG
            this.treeView1.Nodes.Add("ROOT");
            //this.treeView1.Nodes["Root"].
#endif
            this.AllowDrop = true;
            this.textBox1.AllowDrop = true;
            this.treeView1.Visible = false;
            this.textBox1.DragDrop += drop_object;
            this.textBox1.DragEnter += drag_enter;
            this.DragEnter += drag_enter;
            this.DragDrop += drop_object;
        }
    }
}
