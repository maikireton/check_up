using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Threading;
using System.Windows.Threading;
using System.IO.Packaging;
using System.Configuration;




using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace check_up02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new testData();
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string content;
            string[] items = new string[]{};

            //Init the 1st tabItem
            datePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");

            content = ConfigHelper.GetConfig("coprotion");
            items = content.Split(';');
            foreach(string item in items)
            {
                ComboBoxItem itemTmp = new ComboBoxItem();
                itemTmp.Content = item;
                comboBox3.Items.Add(itemTmp);
            }

            //T.B.D 从DB中初始化体检套餐DropList
            content = ConfigHelper.GetConfig("checkup_suits");
            items = content.Split(';');
            foreach (string item in items)
            {
                ComboBoxItem itemTmp = new ComboBoxItem();
                itemTmp.Content = item;
                comboBox2.Items.Add(itemTmp);
            }

            //Init 2st tabItem
            content = ConfigHelper.GetConfig("keshi");
            items = content.Split(';');
            foreach (string item in items)
            {
                ComboBoxItem itemTmp = new ComboBoxItem();
                itemTmp.Content = item;
                comboBox4.Items.Add(itemTmp);
            }

            //Init 3st tabItem
            //T.B.D 从DB中读取科室列表和科室的检查列表
            ListBox ll = new ListBox();
            ListBoxItem llt = new ListBoxItem();
            CheckBox cb = new CheckBox();
            cb.Content = "testCB";
            llt.Content = "Test";
            ll.Items.Add(llt);
            ll.Items.Add(cb);

            ll.Width = 100;
            wrapPanelSuit.Children.Add(ll);

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Trim() == "" ||
                textBox2.Text.Trim() == "" ||
                textBox4.Text.Trim() == "" ||
                comboBox1.Text.Trim() == "" ||
                comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("请将信息添加完整！");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("确定将 " + textBox1.Text + " 添加到预约", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = comboBox3.Text;
                    comboBox3.Items.Add(item);
                    ConfigHelper.SetConfig("coprotion", comboBox3.Text);

                    //T.B.D 将预约记录添加到DB
                }
                else
                { 
                    //do nothing
                }
            }

            //T.B.D SQL insert data to db
           
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text.Trim() != "")
            {
                int age = Convert.ToInt32(textBox2.Text.Trim());
                if (age <= 0 || age >= 120)
                {
                    MessageBox.Show("请输入年龄在0和120之间！");
                    textBox2.Text = "";
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal)
            {
                e.Handled = false;
            }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox4_MouseLeave(object sender, MouseEventArgs e)
        {
            if (textBox4.Text.Trim() != "")
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text.Trim(), @"^[1]+[3,5]+\d{9}"))
                {
                    e.Handled = false;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text.Trim(), @"^(\d{3,4}-)?\d{6,8}$"))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("请输入正确的电话号码! \n手机号请输入:XXX XXXX XXXX \n或者固定电话:XXXX XXXX");
                    this.textBox4.Focus();
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openPicture = new System.Windows.Forms.OpenFileDialog();
            openPicture.Title = "选择文件";
            openPicture.Filter = "*.jpg|*.jpg|*.jpeg|*.jpeg|*.bmp|*.bmp|*.png|*.png";
            openPicture.FileName = string.Empty;
            //openPicture.FilterIndex = 3;
            openPicture.RestoreDirectory = true;
            openPicture.DefaultExt = "jpg";
            System.Windows.Forms.DialogResult result = openPicture.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            
            //show the picture in　windows
            string fileName = openPicture.FileName;
            BitmapImage bitmapImage = new BitmapImage(new Uri(fileName));
            image1.Source = bitmapImage.Clone();

            //save the picture
            JpegBitmapEncoder jpegEn = new JpegBitmapEncoder();
            jpegEn.Frames.Add(BitmapFrame.Create((BitmapSource)image1.Source));

            FileStream pictureFile = new FileStream("001.jpeg", FileMode.Create);
            //StreamWriter mySw = new StreamWriter(pictureFile);
            jpegEn.Save(pictureFile);
            pictureFile.Close();
        }

        private void comboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //Sample Code 在套餐设置中添加科室和检查项
            comboBox5.Text = "";
        }

        private delegate void LoadXpsMethod();
        private FlowDocument m_doc;

        private void TabItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri("CheckUpReport.xaml", UriKind.RelativeOrAbsolute));
            doc.PagePadding = new Thickness(50);
            doc.DataContext = printInfo.m_Data_test;
            m_doc = doc;
            
            //在子线程中执行，延时输出绑定数据。
            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)m_doc).DocumentPaginator);

            //获取这个基于内存的xps document的fixed document
            documetViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }

        private void DocumentViewer_KeyUp_2(object sender, KeyEventArgs e)
        {
            
        }

        private void TabItem_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            //webBrowser.Navigate(@"D:\DaLianYanHuaJiTuan.doc");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //resultCheckInDateGrid.Visibility = Visibility.Hidden;
            PersonalDateCheckInGroupBox.Visibility = Visibility.Visible;
            PersonalDateCheckInGroupBox.IsEnabled = true;
            //dataGridTest.
            //dataGridTest.ItemsSource.Equals({ll, cb});
            //listViewTest.ItemsPanel
            //listViewTest.Items.Add(cb);
            //textBox7.IsEnabled = true;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PersonalDateCheckInGroupBox.Visibility = Visibility.Hidden;
            ListViewItem item = new ListViewItem();
            //item.
            //listViewTest.
            //resultCheckInDateGrid.Visibility = Visibility.Visible;
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            PersonalDateCheckInGroupBox.IsEnabled = false;
        }
    }
}
