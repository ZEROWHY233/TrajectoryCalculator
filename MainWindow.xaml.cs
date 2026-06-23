using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace TrajectoryCalculator
{
    public partial class MainWindow : Window
    {
        private const double SingleRange = 5.0;
        private const int MaxPitch = 60;
        private readonly ObservableCollection<Record> Records;
        private ICollectionView _view;

        public MainWindow()
        {
            InitializeComponent();

            Records = new ObservableCollection<Record>();
            dataGrid.ItemsSource = Records;

            _view = CollectionViewSource.GetDefaultView(Records);
            _view.SortDescriptions.Clear();
            _view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            chargeCombo.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6 };
            ammoCombo.ItemsSource = new string[] { "EMPT", "AP", "HE", "HCHE", "STAR" };
            chargeCombo.SelectedIndex = 0;
            ammoCombo.SelectedIndex = 0;

            int[] fontSizes = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            fontSizeCombo.ItemsSource = fontSizes;
            fontSizeCombo.SelectedItem = 12;
        }

        // ---------- 窗口控制 ----------
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ---------- 版权信息 ----------
        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "弹道计算器 v1.0\n\n" +
                "基于简易弹道模型，仅供学习参考。\n" +
                "© 2026 TrajectoryCalculator",
                "关于",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // ---------- 主题切换 ----------
        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            App.SwitchTheme();
            resultLabel.Foreground = (Brush)Application.Current.Resources["TextBrush"];
        }

        // ---------- 字号 ----------
        private void FontSizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontSizeCombo.SelectedItem is int size)
                App.SetFontSize(size);
        }

        // ---------- 计算 ----------
        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(distanceTextBox.Text, out double distance))
            {
                MessageBox.Show("请输入有效的距离数值。", "输入错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (chargeCombo.SelectedItem == null || ammoCombo.SelectedItem == null)
                return;

            int charge = (int)chargeCombo.SelectedItem;
            string ammo = ammoCombo.SelectedItem.ToString();
            double maxRange = SingleRange * charge;
            double pitch = (distance / maxRange) * MaxPitch;
            bool overRange = distance > maxRange || pitch > MaxPitch;

            if (overRange)
            {
                resultLabel.Content = $"俯仰角：{pitch:F2} ° ⚠️ 超出射程";
                resultLabel.Foreground = Brushes.Red;
                return;
            }
            else
            {
                resultLabel.Content = $"俯仰角：{pitch:F2} °";
                resultLabel.Foreground = (Brush)Application.Current.Resources["TextBrush"];
            }

            int newId = Records.Count > 0 ? Records.Max(r => r.Id) + 1 : 1;
            Record record = new Record
            {
                Id = newId,
                AmmoType = ammo,
                Distance = distance,
                Charge = charge,
                Pitch = pitch,
                Hit = false
            };

            if (!string.IsNullOrWhiteSpace(horTextBox.Text) && double.TryParse(horTextBox.Text, out double hor))
                record.Horizontal = hor;

            Records.Add(record);
            _view.Refresh();
        }

        // ---------- 数据操作（修复多选刷新） ----------
        private void MarkHit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems.Cast<Record>().ToList();
            if (selectedItems.Count == 0) return;

            foreach (Record r in selectedItems)
                r.Hit = !r.Hit;

            // 取消所有选中
            dataGrid.UnselectAll();
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItems.Cast<Record>().ToList();
            foreach (Record r in selected)
                Records.Remove(r);
            _view.Refresh();
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Records.Clear();
            _view.Refresh();
        }

        // ---------- 空白点击取消选中（兼容Ctrl/Shift多选） ----------
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            DependencyObject hitObj = e.OriginalSource as DependencyObject;
            bool clickOnItem = false;

            while (hitObj != null)
            {
                if (hitObj is DataGridRow || hitObj is DataGridCell || hitObj is DataGridColumnHeader)
                {
                    clickOnItem = true;
                    break;
                }
                hitObj = VisualTreeHelper.GetParent(hitObj);
            }

            if (!clickOnItem)
            {
                dataGrid.UnselectAll();
                e.Handled = true;
            }
        }
    }

    // ---------- 数据模型 ----------
    public class Record : INotifyPropertyChanged
    {
        private bool _hit;
        public int Id { get; set; }
        public double? Horizontal { get; set; }
        public double Distance { get; set; }
        public int Charge { get; set; }
        public string AmmoType { get; set; }
        public double Pitch { get; set; }

        public bool Hit
        {
            get => _hit;
            set { _hit = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}