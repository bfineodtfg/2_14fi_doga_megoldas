using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_14fi_doga_megoldas
{
    public partial class Form1 : Form
    {
        int voltage = 12;
        int batteryVoltage = 24;
        Timer moveTimer = new Timer();
        Timer createTimer = new Timer();
        int count = 0;
        List<Electron> electrons = new List<Electron>();
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            button1.Click += IncreaseVoltage;
            button2.Click += DecreaseVoltage;
            StartTimers();
        }
        void StartTimers()
        {
            moveTimer.Interval = 16;
            createTimer.Interval = 500;

            moveTimer.Tick += MoveEvent;
            createTimer.Tick += CreateEvent;

            moveTimer.Start();
            createTimer.Start();
        }
        void MoveEvent(Object s, EventArgs e)
        {
            int divider = 4;
            List<Electron> toBeDeleted = new List<Electron>();
            foreach (Electron item in electrons)
            {
                if (item.Bounds.IntersectsWith(wire1.Bounds) && (item.Top + batteryVoltage / divider) > (wire2.Top - wire2.Height / 2 + item.Height / 2))
                {
                    item.Top -= batteryVoltage / divider;
                }
                else if (item.Bounds.IntersectsWith(wire2.Bounds))
                {
                    item.Left -= batteryVoltage / divider;
                    if (item.Left < voltageRegulator.Right)
                    {
                        item.Hide();
                    }
                }
                else if (item.Bounds.IntersectsWith(voltageRegulator.Bounds))
                {
                    item.Left -= batteryVoltage / divider;
                    if (item.Right <= voltageRegulator.Left)
                    {
                        item.Show();
                        item.speed = voltage / divider;
                    }
                }
                else if (item.Bounds.IntersectsWith(wire3.Bounds))
                {
                    item.Left -= item.speed;
                    if (item.Left < wire3.Left)
                    {
                        item.Hide();
                    }
                }
                else if (item.Bounds.IntersectsWith(light.Bounds))
                {
                    if (item.Left > wire4.Left)
                    {
                        item.Left -= item.speed;
                    }
                    else
                    {
                        item.Top += item.speed;
                        if (item.Top >= light.Bottom)
                        {
                            item.Show();
                        }
                    }
                }
                else if (item.Bounds.IntersectsWith(wire4.Bounds))
                {
                    item.Top += item.speed;
                }
                else if (item.Bounds.IntersectsWith(wire5.Bounds))
                {
                    item.Left += item.speed;
                }
                else if (item.Bounds.IntersectsWith(end.Bounds))
                {
                    if (item.Visible)
                    {
                        item.Hide();
                        count++;
                        UpdateCount();
                        toBeDeleted.Add(item);
                    }
                }
            }
            foreach (Electron item in toBeDeleted)
            {
                electrons.Remove(item);
            }
        }
        void CreateEvent(Object s, EventArgs e)
        {
            Electron electron = new Electron();
            this.Controls.Add(electron);
            electrons.Add(electron);
            electron.AutoSize = false;
            electron.Width = 20;
            electron.Height = 20;
            electron.BackColor = Color.Lime;
            electron.Top = wire1.Bottom - electron.Height;
            electron.Left = wire1.Left + wire1.Width / 2 - electron.Width / 2;
            electron.BringToFront();
        }

        void IncreaseVoltage(Object s, EventArgs e)
        {
            if (voltage < batteryVoltage)
                voltage++;
            UpdateVoltage();
        }
        void DecreaseVoltage(Object s, EventArgs e)
        {
            if (voltage > 4)
                voltage--;
            UpdateVoltage();
        }
        void UpdateCount()
        {
            counterLabel.Text = $"Electrons passed: {count}";
        }
        void UpdateVoltage()
        {
            voltageLabel.Text = voltage + "V";
        }
    }
}
