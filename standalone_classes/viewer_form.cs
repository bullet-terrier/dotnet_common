using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class Document_Viewer : dotnet_forms.Base_Form //Form
{
    TextBox data_box { get; }
    ContextMenuStrip context_box { get; }
    int panel_width { get; }
    int panel_height { get; }
    protected string current_entry { get; set; }
    int current_x_0 { get; set; }
    int current_x_1 { get; set; }
    int current_y_0 { get; set; }
    int current_y_1 { get; set; }
    int box_x_0 { get; set; }
    int box_x_1 { get; set; }
    int box_y_0 { get; set; }
    int box_y_1 { get; set; }

    int current_lines { get; set; }
    float px_per_line { get; set; } // all display elements.

    // get global positions.
    #region GPS
    // export these into a template/class library object.
    private int get_global_y(Control element)
    {
        int y = element.Location.Y;
        y += this.Location.Y;
        return y;
    }

    // get a global X coordinate in relation to the current location.
    private int get_global_x(Control element)
    {
        int x = element.Location.X;
        x += this.Location.X;
        return x;
    }

    private int get_global_y1(Control element)
    {
        int y0 = get_global_y(element);
        int y1 = y0 + element.Height;
        return y1;
    }

    private int get_global_x1(Control element)
    {
        int x0 = get_global_x(element);
        int x1 = x0 + element.Width;
        return x1;
    }
    #endregion

    #region LOCAL
    private int get_local_x(Control element, Point comparator)
    {
        int base_x = get_global_x(element);
        //
        // We'll need to handle the comparison like this then.
        return comparator.X - base_x;
        //return 0;
    }

    private int get_local_y(Control element, Point comparator)
    {
        int base_y = get_global_y(element);
        return comparator.Y - base_y;
    }

    private int get_local_x1(Control element, Point comparator)
    {
        return 0;
    }

    private int get_local_y1(Control element, Point comparator)
    {
        return 0;
    }
    #endregion

    #region

    private void get_current_line(TextBox element)
    {
        // this gets a bit closer - looks like there's a little bit of a lateral shift.
        // alright - we'll need to get the current display as well as the
        // 
        // need to get the type of the element I suppose.
        // so in this case - I'm getting it based on cursor.position when relative to the screen.
        //
        /*
         * What method should I handle 
         */
        Point translated_point = new Point(get_local_x(element, Cursor.Position), get_local_y(element, Cursor.Position));

        var a = element.GetCharFromPosition(translated_point);
        this.current_entry = element.Lines[element.GetLineFromCharIndex(element.GetFirstCharIndexOfCurrentLine())];
#if DEBUG
            
            Console.WriteLine(a);
            Console.WriteLine($"GX:{Cursor.Position.X},LX:{get_local_x(element,Cursor.Position)}\tGY:{Cursor.Position.Y},LY{get_local_y(element,Cursor.Position)}");
            Console.WriteLine(element.GetLineFromCharIndex(element.Text.IndexOf(a)));
            Console.WriteLine(this.current_entry);
#endif
        //element.GetLineFromCharIndex()
    }

    #endregion

    // defaulting this might come back to byte me. lol.
    public void set_display(string content = "", string name = "")
    {
        this.textBox1.Text = content;
        this.Text = name; // name will simply be a construct.
    }

    public string get_display()
    {
        // should return the string for use.
        return this.textBox1.Text;
    }

    // 
    protected void viewbox_location_changed(object sender, EventArgs e)
    {
    }

    protected void textbox_1_click(object sender, EventArgs e)
    {
#if DEBUG_OLD
            // need to have the most up to date output.
            // I've got to handle the screen offset - seems like the textbox location is local to the application window.
            Console.WriteLine(Cursor.Position);
            Console.WriteLine($"Box X:{this.textBox1.Location.X}\t Box Y:{this.textBox1.Location.Y}\tBox Height: {this.textBox1.Height}\tBox Width: {this.textBox1.Width}");
            if(Cursor.Position.X >= this.textBox1.Location.X && Cursor.Position.X <= this.textBox1.Location.X+this.textBox1.Width)
            {
                Console.WriteLine("Horizontal OK");
            }
            if(Cursor.Position.Y >= this.textBox1.Location.Y && Cursor.Position.Y <= this.textBox1.Location.Y+this.textBox1.Height)
            {
                Console.WriteLine("Vertical OK");
            }
            if (textBox1.DisplayRectangle.Contains(Cursor.Position))
            {
                Console.WriteLine("Position contained. Vertical and Horizontal OK.");
            }
            Console.WriteLine($"BORDER:{this.Location} TEXTBOX: {this.textBox1.Location}");
#elif DEBUG
            Console.WriteLine($"Global X axis:{get_global_x(this.textBox1)}-{get_global_x1(this.textBox1)}\tGlobal Y axis:{get_global_y(this.textBox1)}-{get_global_y1(this.textBox1)}");

            if (Cursor.Position.X >= get_global_x(this.textBox1) && Cursor.Position.X <= get_global_x1(this.textBox1))
            {
                Console.WriteLine("Within X bounds for textbox.");
            }
            if(Cursor.Position.Y >= get_global_y(this.textBox1) && Cursor.Position.Y <= get_global_y1(this.textBox1))
            {
                Console.WriteLine("Within Y bounds for textbox.");
            }
            Console.WriteLine(Cursor.Position);

            // works on click - needs to be adjusted
            get_current_line(this.textBox1); // let's see what that does.

            
#endif
    }

    protected void select_line(object sender, ColumnClickEventArgs e)
    {
    }

    // we'll worry about this a little less - though i should see where I left off with this.
    protected void resize_box(object sender, EventArgs e)
    {
        //this.data_box.Bounds = this.Bounds;
        //this.data_box.Bounds.Width = this.Width - data_box.ScrollBars.w
        //this.data_box.Bounds = this.Bounds; // need to get the width/height values established correctly.
        //this.data_box.SetBounds(this.Bounds.X + panel_width, this.Bounds.Y + panel_height, this.Width - 2 * panel_width, this.Height - 2 * panel_height);
        //this.data_box.Site = this.Site;
        this.data_box.Bounds = this.panel1.Bounds;
    }

    public Document_Viewer()
    {
        InitializeComponent();

        this.data_box = this.textBox1;

        // enable to allow some changes to be made as the user desires.
        this.context_box = this.contextMenuStrip1;

        data_box.Visible = true;
        //this.data_box.Top = this.Top;
        this.data_box.Multiline = true;
        this.data_box.ScrollBars = ScrollBars.Both;
        // 
        this.data_box.Width = this.Bounds.Width - (2 * panel_width);
        this.data_box.Height = this.Height - (2 * panel_height);
        //
        this.data_box.Show(); // show on initialization
        this.data_box.BringToFront();
        this.data_box.WordWrap = false;

        this.data_box.Bounds = this.panel1.Bounds;

        this.data_box.ReadOnly = true; // force readonly.

        this.Resize += this.resize_box; //
                                        //this.Resize += 
        this.textBox1.Click += textbox_1_click;
#if DEBUG
            for(int i = 0;i<200; i++)
            {
                this.textBox1.Text += $"ALPHA:_{i}___\r\n";
            }
#endif
    }
}