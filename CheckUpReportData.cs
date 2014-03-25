using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;

namespace check_up02
{
    public class PrintInfoTitle
    {
        public string indexNo { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string checkUpDate { get; set; }
        public string checkUpSuit { get; set; }
    }


    public class keShiToCheckItems
    {
        public TextBox keShiName;
        public List<ComboBoxItem> CheckItems;
    }

    public static class printInfo
    {
        public static PrintInfoTitle m_Data_test = new PrintInfoTitle
        {
            indexNo = "100100",
            name = "张三",
            sex = "男",
            checkUpDate = "2014-02-03",
            checkUpSuit = "体检套餐一"
        };
    }

    //科室所有的诊断结果List
    public class Keshi_CheckIn_Result
    {
        // 肝脏     未见异常
        public string checkItem { get; set; }
        public List<ComboBoxItem> checkItemResults { get; set; }
    }

    public enum OrderStatus { None, New, Processing, Shipped, Received };


    public class testData
    {
        public ICollectionView Result_KeshiToItems { get; private set; }

        public testData()
        {
            var keshi_check_in_result = new List<Keshi_CheckIn_Result>
                                            {
                                                new Keshi_CheckIn_Result
                                                {
                                                    checkItem = "内科检查",
                                                    checkItemResults = new List<ComboBoxItem>
                                                    {
                                                        new ComboBoxItem 
                                                        {
                                                            Content = "未见异常"
                                                        },
                                                        new ComboBoxItem
                                                        {
                                                            Content = "肝脏受损"
                                                        }
                                                    }
                                                },
                                                 new Keshi_CheckIn_Result
                                                {
                                                    checkItem = "外科检查",
                                                    checkItemResults = new List<ComboBoxItem>
                                                    {
                                                        new ComboBoxItem
                                                        {
                                                            Content = "未见异常"
                                                        },
                                                        new ComboBoxItem
                                                        {
                                                            Content = "肝脏受损"
                                                        }
                                                    }
                                                }
                                            };

            Result_KeshiToItems = CollectionViewSource.GetDefaultView(keshi_check_in_result);
        }
    }

}
