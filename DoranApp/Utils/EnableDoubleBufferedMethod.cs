using System;
using System.Reflection;
using System.Windows.Forms;

namespace DoranApp.Utils;

public static class EnableDoubleBufferedMethod
{
    public static void EnableDoubleBuffered(this DataGridView dgv, bool setting)
    {
        Type dgvType = dgv.GetType();
        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        pi.SetValue(dgv, setting, null);
    }
}