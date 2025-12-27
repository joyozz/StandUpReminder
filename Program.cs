using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace StandUpReminder;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new ReminderApp());
    }
}

class ReminderApp : ApplicationContext
{
    private NotifyIcon trayIcon;
    private System.Windows.Forms.Timer timer;
    private ReminderForm? reminderForm;
    private Random random = new();
    public int Countdown { get; private set; } = 600;

    public ReminderApp()
    {
        trayIcon = new()
        {
            Icon = CreateIcon(),
            Text = "Stand Up Reminder - Double click to show reminder, right-click to exit",
            ContextMenuStrip = new()
        };
        var startupItem = new ToolStripMenuItem("Auto Start on Boot", null, (s, e) => ToggleAutoStart());
        startupItem.Checked = IsAutoStartEnabled();
        trayIcon.ContextMenuStrip.Items.Add(startupItem);
        trayIcon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => Exit());
        trayIcon.DoubleClick += (s, e) => ShowReminder();
        trayIcon.Visible = true;

        timer = new() { Interval = 1000 };
        timer.Tick += OnTick;
        timer.Start();

        ScheduleNextReminder();
    }

    private void ScheduleNextReminder()
    {
        int minutes = random.Next(40, 61);
        timer.Interval = minutes * 60 * 1000;
    }

    private void OnTick(object? sender, EventArgs e)
    {
        if (Countdown > 0)
        {
            Countdown--;
            if (reminderForm != null && !reminderForm.IsDisposed)
                reminderForm.UpdateCountdown(Countdown);
        }
        else
        {
            if (reminderForm != null && !reminderForm.IsDisposed)
            {
                reminderForm.Close();
                reminderForm.Dispose();
                reminderForm = null!;
            }
            ScheduleNextReminder();
        }
    }

    private void ShowReminder()
    {
        reminderForm?.Close();
        reminderForm = new ReminderForm();
        reminderForm.UpdateCountdown(Countdown);
        reminderForm.Show();
    }

    private void ToggleAutoStart()
    {
        if (IsAutoStartEnabled())
            DisableAutoStart();
        else
            EnableAutoStart();
    }

    private static void EnableAutoStart()
    {
        string exePath = Process.GetCurrentProcess().MainModule?.FileName ?? Application.ExecutablePath;
        using Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        if (key != null)
            key.SetValue("StandUpReminder", exePath);
    }

    private static void DisableAutoStart()
    {
        using Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        if (key != null)
            key.DeleteValue("StandUpReminder", false);
    }

    private static bool IsAutoStartEnabled()
    {
        using Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
        return key?.GetValue("StandUpReminder") != null;
    }

    private void Exit()
    {
        trayIcon.Visible = false;
        Application.Exit();
    }

    public static Icon CreateIcon()
    {
        using Bitmap bmp = new(32, 32);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.Transparent);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        g.FillPath(Brushes.Blue, CreateRoundedRect(2, 2, 28, 28, 6));

        int cx = 16, headY = 8, bodyTop = 12, bodyBottom = 22, legBottom = 28;

        g.FillEllipse(Brushes.White, cx - 5, headY - 5, 10, 10);

        using Pen pen = new Pen(Color.White, 3);
        g.DrawLine(pen, cx, bodyTop, cx, bodyBottom);
        g.DrawLine(pen, cx - 6, bodyTop + 4, cx + 6, bodyTop + 4);
        g.DrawLine(pen, cx - 4, bodyBottom, cx - 4, legBottom);
        g.DrawLine(pen, cx + 4, bodyBottom, cx + 4, legBottom);

        return Icon.FromHandle(bmp.GetHicon());
    }

    private static GraphicsPath CreateRoundedRect(float x, float y, float width, float height, float radius)
    {
        GraphicsPath path = new();
        path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
        path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
        path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
        path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
        path.CloseFigure();
        return path;
    }
}

class ReminderForm : Form
{
    private Label lblCountdown = null!;
    private Label lblText = null!;
    private PictureBox picBox = null!;
    private System.Windows.Forms.Timer? animTimer;
    private int frameIndex;

    public ReminderForm()
    {
        Icon = ReminderApp.CreateIcon();
        Text = "Stand Up Reminder";
        Size = new Size(450, 380);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        BackColor = Color.FromArgb(30, 100, 180);

        picBox = new()
        {
            Location = new Point(150, 20),
            Size = new Size(150, 100),
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        picBox.Image = CreateCowFrame(0);

        lblText = new Label()
        {
            Text = "Stand up and stretch a bit—you'll feel ten minutes younger!",
            AutoSize = false,
            Size = new Size(420, 60),
            Location = new Point(15, 130),
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Sans-serif", 12, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent
        };

        lblCountdown = new Label()
        {
            Text = "Time reclaimed: -- seconds",
            AutoSize = false,
            Size = new Size(420, 40),
            Location = new Point(15, 200),
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 18, FontStyle.Bold),
            ForeColor = Color.Yellow
        };

        Controls.AddRange([picBox, lblText, lblCountdown]);

        animTimer = new() { Interval = 50 };
        animTimer.Tick += OnAnimTick;
        animTimer.Start();
    }

    private void OnAnimTick(object? sender, EventArgs e)
    {
        frameIndex++;
        picBox.Image = CreateCowFrame(frameIndex);
    }

    public void UpdateCountdown(int count)
    {
        lblCountdown.Text = $"Time reclaimed: {count} seconds";
    }

    private static Bitmap CreateCowFrame(int frame)
    {
        Bitmap bmp = new(150, 100);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.Transparent);

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;

        int cx = 75, cy = 50, radius = 40;

        g.FillEllipse(Brushes.LightYellow, cx - radius, cy - radius, radius * 2, radius * 2);
        g.DrawEllipse(new Pen(Color.DarkGray, 3), cx - radius, cy - radius, radius * 2, radius * 2);

        g.FillEllipse(Brushes.White, cx - 3, cy - 3, 6, 6);

        for (int i = 0; i < 12; i++)
        {
            double angle = -i * Math.PI / 6;
            int x1 = cx + (int)(Math.Cos(angle) * (radius - 5));
            int y1 = cy + (int)(Math.Sin(angle) * (radius - 5));
            int x2 = cx + (int)(Math.Cos(angle) * (radius - 10));
            int y2 = cy + (int)(Math.Sin(angle) * (radius - 10));
            g.DrawLine(new Pen(Color.DarkGray, i % 3 == 0 ? 2 : 1), x1, y1, x2, y2);
        }

        double secAngle = -frame * Math.PI / 30;
        double minAngle = secAngle / 60;
        double hourAngle = minAngle / 12;

        g.DrawLine(new Pen(Color.Red, 2), cx, cy,
            cx + (int)(Math.Cos(secAngle) * (radius - 8)),
            cy + (int)(Math.Sin(secAngle) * (radius - 8)));

        g.DrawLine(new Pen(Color.Blue, 3), cx, cy,
            cx + (int)(Math.Cos(minAngle) * (radius - 20)),
            cy + (int)(Math.Sin(minAngle) * (radius - 20)));

        g.DrawLine(new Pen(Color.Green, 4), cx, cy,
            cx + (int)(Math.Cos(hourAngle) * (radius - 28)),
            cy + (int)(Math.Sin(hourAngle) * (radius - 28)));

        return bmp;
    }
}
