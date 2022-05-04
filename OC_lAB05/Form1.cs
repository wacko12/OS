using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Threading;
using Timer = System.Threading.Timer;

namespace OC_lAB05_PCB
{

    public partial class Form1 : Form
    {
        //Снимок всех процессов
        private List<Process> processes = null;
        private ListViewItemComparer comparer = null;

        public Form1()
        {
            InitializeComponent();
        }
        //Заполнение и обновление списка
        private void GetProcesses()
        {
            processes.Clear();
            processes = Process.GetProcesses().ToList<Process>();
        }
        //Отображение списка
        private void RefreshProcessesList()
        {
            double memSize = 0;
            listView1.Items.Clear();
            foreach(Process p in processes)
            {
                memSize = 0;
                PerformanceCounter pc = new PerformanceCounter();
                pc.CategoryName = "Process";
                pc.CounterName = "Working Set - Private";
                pc.InstanceName = p.ProcessName;
                memSize = (double)pc.NextValue() / (1000 * 1000);
                string[] row = new string[] { p.ProcessName.ToString(), Math.Round(memSize, 1).ToString() };
                listView1.Items.Add(new ListViewItem(row));
                pc.Close();
                pc.Dispose();
            }
            Text = "Запущено процессов" + processes.Count.ToString();
        }
        //Фильтры
        private void RefreshProcessesList(List<Process> processes, string keyword)
        {
            try
            {
                double memSize = 0;
                listView1.Items.Clear();
                foreach (Process p in processes)
                {
                    if (p != null)
                    {
                        memSize = 0;
                        PerformanceCounter pc = new PerformanceCounter();
                        pc.CategoryName = "Process";
                        pc.CounterName = "Working Set - Private";
                        pc.InstanceName = p.ProcessName;
                        memSize = (double)pc.NextValue() / (1000 * 1000);
                        string[] row = new string[] { p.ProcessName.ToString(), Math.Round(memSize, 1).ToString() };
                        listView1.Items.Add(new ListViewItem(row));
                        pc.Close();
                        pc.Dispose();
                    }
                }
                Text = $"Запущено процессов '{keyword}'" + processes.Count.ToString();
            }
            catch (Exception) { }

        }
        //Завершение процесса
        private void KillProcess(Process process)
        {
            process.Kill();
            process.WaitForExit();
        }
        //Завершение древа процессов
        private void KillProcessAndChildren(int pid)
        {
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "Select * From Win32_Process Where ParentProcessID" + pid);
            ManagementObjectCollection objectCollection = searcher.Get();
            foreach(ManagementObject obj in objectCollection)
            {
                KillProcessAndChildren(Convert.ToInt32(obj["ProcessID"]));
            }
            try
            {
                Process p = Process.GetProcessById(pid);
                p.Kill();
                p.WaitForExit();
            }
            catch (ArgumentException) { }
        }
        //Получение id родителя
        private int GetParentProcessId(Process p)
        {
            int paarentID = 0;
            try
            {
                ManagementObject managementObject = new ManagementObject("win32_process.handle='" + p.Id + "'");
                managementObject.Get();
                paarentID = Convert.ToInt32(managementObject["ParentProcessId"]);
            }
            catch (Exception) { }
            return paarentID;
        }
        //Отображение процессов
        private void Form1_Load(object sender, EventArgs e)
        {
            processes = new List<Process>();
            GetProcesses();
            RefreshProcessesList();
            comparer = new ListViewItemComparer();
            comparer.ColumnIndex = 0;
        }
        //Кнопка обновить
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GetProcesses();
            RefreshProcessesList();
        }
        //Кнопка приостановить
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToSuspend = processes.Where((x) => x.ProcessName ==
                    listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    StartStop.Suspend(processToSuspend);
                    GetProcesses();
                    RefreshProcessesList();
                }
            }
            catch (Exception) { }
        }
        //Кнопка приостановить через время
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(5000);
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToSuspend = processes.Where((x) => x.ProcessName ==
        listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    StartStop.Suspend(processToSuspend);
                    GetProcesses();
                    RefreshProcessesList();

                }
            }
            catch (Exception) { }
        }
        //Кнопка возобновить
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToResume = processes.Where((x) => x.ProcessName ==
                    listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    StartStop.Resume(processToResume);
                    GetProcesses();
                    RefreshProcessesList();
                }
            }
            catch (Exception) { }
        }
        //Кнопка завершить
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToKill = processes.Where((x) => x.ProcessName ==
                    listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    KillProcess(processToKill);
                    GetProcesses();
                    RefreshProcessesList();
                }
            }
            catch (Exception) { }
        }
        //Кнопка завершить древо
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToKill = processes.Where((x) => x.ProcessName ==
                    listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    KillProcessAndChildren(GetParentProcessId(processToKill));
                    GetProcesses();
                    RefreshProcessesList();
                }
            }
            catch (Exception) { }
        }
        //Кнопка завершить древо(всплывающая)
        private void завершитьДревоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0] != null)
                {
                    Process processToKill = processes.Where((x) => x.ProcessName ==
                    listView1.SelectedItems[0].SubItems[0].Text).ToList()[0];
                    KillProcessAndChildren(GetParentProcessId(processToKill));
                    GetProcesses();
                    RefreshProcessesList();
                }
            }
            catch (Exception) { }
        }
        //Кнопка запустить задачу(всплывающая)
        private void запуститьЗадачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Interaction.InputBox("Введите имя программы", "Запуск новой задачи");
            try
            {
                Process.Start(path);
            }
            catch (Exception) { }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            GetProcesses();
            List<Process> filteredprocesses = processes.Where((x) => x.ProcessName.ToLower().Contains(toolStripTextBox1.Text.ToLower())).ToList<Process>();
            RefreshProcessesList(filteredprocesses, toolStripTextBox1.Text);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            comparer.ColumnIndex = e.Column;
            comparer.SortDirection = comparer.SortDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            listView1.ListViewItemSorter = comparer;
            listView1.Sort();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
