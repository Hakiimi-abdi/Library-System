using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; set; } = true;


        public static void ApplyTheme(Form form)
        {
            if (IsDarkMode)
            {
                form.BackColor = Color.FromArgb(30, 30, 30);
                foreach (Control ctrl in form.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        btn.BackColor = Color.FromArgb(50, 50, 50);
                        btn.ForeColor = Color.White;
                    }
                    else if (ctrl is Panel panel)
                    {
                        panel.BackColor = Color.FromArgb(45, 45, 45);
                    }
                    else if (ctrl is Label lbl)
                    {
                        lbl.ForeColor = Color.White;
                    }
                    else if (ctrl is TextBox txt)
                    {
                        txt.BackColor = Color.FromArgb(50, 50, 50);
                        txt.ForeColor = Color.White;
                    }
                }
            }
            else
            {
                form.BackColor = Color.White;
                foreach (Control ctrl in form.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        btn.BackColor = Color.White;
                        btn.ForeColor = Color.Black;
                    }
                    else if (ctrl is Panel panel)
                    {
                        panel.BackColor = Color.LightGray;
                    }
                    else if (ctrl is Label lbl)
                    {
                        lbl.ForeColor = Color.Black;
                    }
                    else if (ctrl is TextBox txt)
                    {
                        txt.BackColor = Color.White;
                        txt.ForeColor = Color.Black;
                    }
                }
            }
        }
    }


}
