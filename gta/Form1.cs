using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gta
{
    public partial class Form1 : Form
    {
        KeyboardHook hook = new KeyboardHook();
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
        Boolean registered = false;
        int pressed = 0;
        public Form1()
        {
            InitializeComponent();
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            try
            {
                var process = Process.GetProcessesByName("GTA5")[0];
                process.Suspend();
                Thread.Sleep(10000);
                process.Resume();
            }catch(Exception ex)
            {
                textBox2.Clear();
                textBox2.Text = "Could not find active instance of GTA5.exe. Debug:\n" + ex.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var process = Process.GetProcessesByName("GTA5")[0];
            process.Suspend();
            Thread.Sleep(10000);
            process.Resume();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var process = Process.GetProcessesByName("GTA5")[0];
            process.Suspend();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var process = Process.GetProcessesByName("GTA5")[0];
            process.Resume();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(registered)
                    hook.UnregisterHotKey();
                else
                    hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
                Keys key1 = (Keys)Enum.Parse(typeof(Keys), textBox1.Text, true);
                hook.RegisterHotKey(gta.ModifierKeys.Control, key1);
                registered = true;
                textBox2.Clear();
                textBox2.Text = "Registered CTRL + " + textBox1.Text.ToUpper() + ".";
            }
            catch (Exception ex)
            {
                textBox2.Clear();
                textBox2.Text = "Could not register hotkey, try using a different key. Debug:\n" + ex.ToString();
            }
        }
    }
}
