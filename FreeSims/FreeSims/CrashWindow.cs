using System;
using Gtk;
using System.Diagnostics;
using System.Reflection;

namespace Technochips.FreeSims
{
	public partial class CrashWindow : Gtk.Window
    {
		Exception e;
		string fullerror;
		string title;
		string body;

		public static void Run(Exception e)
		{
			Application.Init();
			var cw = new CrashWindow();
			cw.SetException(e);
			Application.Run();
		}
		public CrashWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
		public void SetException(Exception e)
		{
			this.e = e;
			fullerror = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + Environment.NewLine + Environment.OSVersion.VersionString + Environment.NewLine + e.ToString()}";
			label1.LabelProp = $"Looks like FreeSims just crashed. You should report this on the GitHub page\nException code: {e.GetType().Name}\nFull crash info:";
			textview1.Buffer.Text = fullerror;

			title = $"There was a crash: {e.GetType().Name}";
			body = $"Error code: `{e.GetType().Name}`\n## Steps to crash\n1. Step\n2. Step\n3. Step\n_Edit the step to how you crashed_\n## More info\n```\n{fullerror}\n```\n\n_You should add a description, remove the italic texts and edit title and most of the things, to make it more alive, and to provide more details to help the developer._";
		}

		protected void exit(System.Object sender, System.EventArgs e)
		{
			this.Destroy();
			Application.Quit();
		}

		protected void reportBug(System.Object sender, System.EventArgs e)
		{
			Process.Start($"https://github.com/Technochips/freesims/issues/new?title={title.Replace(" ", "%20").Replace("\"", "%22")}&body={body.Replace(" ", "%20").Replace("#", "%23").Replace("\n", "%0A")}");
		}
	}
}
