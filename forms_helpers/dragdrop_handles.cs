// we'll use this to simplify the output.
// drag_enter then drag_drop events.
namespace Forms_Helpers
{

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

                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop); // gotcha - that's why my cast was screwy.

                //DataFormats.FileDrop.

                for (int i = 0; i < filenames.Length; i++)
                {
                    this.textBox1.Text += File.ReadAllText(filenames[i]);
                }
                this.textBox1.ScrollBars = ScrollBars.Both;
                
                // this might work, if it does I'll be happy and use this to build a proto compiler.

                
                    
            }
            
        }
}