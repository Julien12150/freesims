
// This file has been generated by the GUI designer. Do not modify.
namespace Technochips.FreeSims
{
	public partial class CrashWindow
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Label label1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TextView textview1;

		private global::Gtk.HButtonBox hbuttonbox1;

		private global::Gtk.Button button1;

		private global::Gtk.Button button2;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Technochips.FreeSims.CrashWindow
			this.Name = "Technochips.FreeSims.CrashWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("FreeSims has crashed.");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Technochips.FreeSims.CrashWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("label1");
			this.vbox1.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.textview1 = new global::Gtk.TextView();
			this.textview1.CanFocus = true;
			this.textview1.Name = "textview1";
			this.textview1.Editable = false;
			this.GtkScrolledWindow.Add(this.textview1);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w3.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbuttonbox1 = new global::Gtk.HButtonBox();
			this.hbuttonbox1.Name = "hbuttonbox1";
			// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.button1 = new global::Gtk.Button();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString("Report bug");
			this.hbuttonbox1.Add(this.button1);
			global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.button1]));
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.button2 = new global::Gtk.Button();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Label = global::Mono.Unix.Catalog.GetString("Exit");
			this.hbuttonbox1.Add(this.button2);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.button2]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox1.Add(this.hbuttonbox1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbuttonbox1]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.button1.Clicked += new global::System.EventHandler(this.reportBug);
			this.button2.Clicked += new global::System.EventHandler(this.exit);
		}
	}
}
