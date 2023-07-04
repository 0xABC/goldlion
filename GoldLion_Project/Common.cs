using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using Sylvan.Data.Excel;

namespace GoldLion_Project
{
    public static class Common
    {
        /// <summary>
        /// 读取指定Order Form的数据
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static Task<OrderData[]> ReadOrderFormDataAsync(string strPath, IProgress<string> progress)
        {
            var t = Task.Run(() =>
            {
                var resList = new List<OrderData>();

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                // var excelFile = new FileInfo(strPath);
                //
                // if (IsFileLocked(excelFile))
                // {
                //     progress.Report($"该Order Form被其他程序占用，无法打开，已跳过！-> {Path.GetFileName(strPath)}");
                //     return resList.ToArray();
                // }

                //只读取内容 建议使用FileStream
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (var package = new ExcelPackage(fs))
                {
                    for (int j = 0; j < package.Workbook.Worksheets.Count; j++)
                    {
                        var sh = package.Workbook.Worksheets[j];
                        if (sh.Hidden == eWorkSheetHidden.Visible)
                        {
                            Regex rgx1 = new Regex("^bill to code|^bill to site", RegexOptions.IgnoreCase);
                            var query1 = from cell in sh.Cells["1:19"]
                                where rgx1.IsMatch(cell.Value?.ToString() ?? string.Empty)
                                select cell;

                            if (query1.Count() == 2)
                            {
                                //获取bill to code
                                foreach (var rng in query1)
                                {
                                    if (rng.Value.ToString().ToLower().Contains("code"))
                                    {
                                        string billCode = rng.Offset(0, 1).Value.ToString().Trim();
                                        if (billCode == "897217" || billCode == "998003" || billCode == "1007627")
                                        {
                                            int titleRow = (from cell in sh.Cells["20:25"]
                                                where (cell.Value?.ToString() ?? string.Empty).Contains("订单号")
                                                select cell).First().Start.Row;

                                            int orderNoCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "订单号");
                                            int materialCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "辅料编号");
                                            int cusItemCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "customer item");
                                            int averyItemCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "avery item");
                                            int qtyCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "数量");
                                            int priceCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "单价");
                                            int unitPriceCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "unit price");
                                            int systemCol = MyGetCol(sh.Cells[titleRow + ":" + titleRow], "system");

                                            //其中一个Column为0 直接跳过读取这个sheet
                                            if (new[]
                                                {
                                                    orderNoCol, materialCol, cusItemCol, averyItemCol,
                                                    qtyCol, priceCol, unitPriceCol, systemCol
                                                }.Contains(0))
                                            {
                                                continue;
                                            }

                                            for (int i = titleRow + 1; i <= sh.Dimension.End.Row; i++)
                                            {
                                                if ((sh.Cells[i, orderNoCol].Value?.ToString().Trim() ?? string.Empty) == string.Empty)
                                                    continue;

                                                int curQty = 0;
                                                if (int.TryParse((sh.Cells[i, qtyCol].Value?.ToString() ?? string.Empty), out curQty))
                                                {
                                                    double curPrice = 0;
                                                    double.TryParse((sh.Cells[i, priceCol].Value?.ToString() ?? string.Empty),
                                                        out curPrice);

                                                    double curUnitPrice = 0;
                                                    double.TryParse((sh.Cells[i, unitPriceCol].Value?.ToString() ?? string.Empty),
                                                        out curPrice);

                                                    resList.Add(new OrderData()
                                                    {
                                                        AveryItem = sh.Cells[i, averyItemCol].Value?.ToString().Trim() ?? string.Empty,
                                                        CustomerItem = sh.Cells[i, cusItemCol].Value?.ToString().Trim() ?? string.Empty,
                                                        MaterialNo = sh.Cells[i, materialCol].Value?.ToString().Trim() ?? string.Empty,
                                                        OrderNo = sh.Cells[i, orderNoCol].Value?.ToString().Trim() ?? string.Empty,
                                                        Price = curPrice,
                                                        Qty = curQty,
                                                        System = sh.Cells[i, systemCol].Value?.ToString().Trim() ?? string.Empty,
                                                        UnitPrice = curUnitPrice,
                                                        CompanyName = sh.Name.Trim(),
                                                        ParentFolderName = Path.GetFileName(Path.GetDirectoryName(strPath)),
                                                        Sol = string.Empty
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return resList.ToArray();
            });
            return t;
        }

        /// <summary>
        /// 异步读取 open so report 数据
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static Task<List<ReportData>> ReadReportDataAsync(string strPath)
        {
            var t = Task.Run(() =>
            {
                List<ReportData> resList = new List<ReportData>();

                //用这个工具读xlsb文件性能不错
                //逐行读取
                //只读第一个sheet   open so只有一个sheet
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (ExcelDataReader reader = ExcelDataReader.Create(fs, ExcelWorkbookType.ExcelBinary))
                {
                    const int soldCol = 1 - 1;
                    const int poCol = 15 - 1;
                    const int itemCol = 47 - 1;
                    const int qtyCol = 35 - 1;
                    const int orderNumberCol = 13 - 1;
                    const int lineNumberCol = 30 - 1;

                    //跳过第一行 标题行
                    while (reader.Read())
                    {
                        //判断sold to number
                        if (reader.GetString(soldCol).Trim() != "888133") continue;

                        int curQty = 0;
                        int.TryParse(reader.GetString(qtyCol).Trim(), out curQty);
                        resList.Add(new ReportData()
                        {
                            Po = reader.GetString(poCol).Trim(),
                            Item = reader.GetString(itemCol).Trim(),
                            Qty = curQty,
                            Sol = reader.GetString(orderNumberCol).Trim() + "-" + reader.GetString(lineNumberCol).Trim()
                        });
                    }
                }

                return resList;
            });
            return t;
        }


        /// <summary>
        /// 将读取的数据 写入一个新的Excel里面
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="reportFileList"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static Task<string> WriteData(List<OrderData> orderList, List<string> reportFileList, IProgress<string> progress)
        {
            //写入新Excel 可以用 FileInfo
            FileInfo excelFile = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +
                                              DateTime.Now.ToString("yyyymmddhhmmss") + ".xlsx");

            var t = Task.Run(async () =>
            {
                //遍历SO Report，直到所有订单数据都匹配出SOL 或者 所有 SO Report 都读取过一次
                foreach (var reportPath in reportFileList)
                {
                    //筛选出sol为空的订单数据
                    var emptySolQuery = (from oData in orderList
                        where oData.Sol == string.Empty
                        select oData);

                    if (!emptySolQuery.Any()) //如果全部sol都已经找到，退出循环
                        break;

                    progress.Report($"正在读取Open So Report -> {Path.GetFileName(reportPath)}");
                    var tmpFile = new FileInfo(reportPath);
                    if (IsFileLocked(tmpFile))
                    {
                        progress.Report($"该Open So Report被其他程序占用，无法打开，已跳过！ -> {Path.GetFileName(reportPath)}");
                        continue;
                    }

                    var rpDataList = await ReadReportDataAsync(reportPath);
                    foreach (var oData in emptySolQuery)
                    {
                        //先查询 item+qty 相同的数据
                        var query2 = (from rpData in rpDataList
                            where oData.AveryItem == rpData.Item && oData.Qty == rpData.Qty
                            select rpData);

                        //查询到数据 可能有多条
                        if (query2.Any())
                        {
                            var sp1 = oData.OrderNo.Split('+');
                            if (sp1.Length >= 2)
                            {
                                var query3 = (from rpData in query2
                                    where rpData.Po.Contains(sp1[0]) && rpData.Po.Contains(sp1[1])
                                    select rpData);

                                if (query3.Count() == 1)
                                {
                                    oData.Sol = query3.First().Sol;
                                }
                                else if (query3.Count() > 1)
                                {
                                    if (sp1.Length >= 3)
                                    {
                                        //获取相似度最高的一项内容  Net Core这里可以改写为MaxBy，更加简洁
                                        oData.Sol = query3.OrderByDescending(
                                            item => CalculateSimilarity(sp1[2], item.Po)).First().Sol;
                                    }
                                    else
                                    {
                                        foreach (var rpData in query3)
                                        {
                                            oData.Sol += rpData.Sol + "/";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //新建一个Excel并写入
                using (var package = new ExcelPackage(excelFile))
                {
                    var sh = package.Workbook.Worksheets.Add("Data");
                    int wtRow = 2;
                    sh.Cells[1, 1, 1, 14].LoadFromArrays(new List<object[]>
                    {
                        new object[]
                        {
                            "日期", "订单号", "辅料编号", "Customer Item", "Avery Item", "数量", "单价",
                            "Unit Price", "System", "SOL", "AWB", "收货地", "签收通知记录", "工厂回签确认"
                        }
                    });

                    foreach (var data in orderList)
                    {
                        sh.Cells[wtRow, 1, 1, 12].LoadFromArrays(new List<object[]>
                        {
                            new object[]
                            {
                                data.ParentFolderName, data.OrderNo, data.MaterialNo, data.CustomerItem, data.AveryItem,
                                data.Qty, data.Price, data.UnitPrice, data.System, data.Sol, string.Empty, data.CompanyName
                            }
                        });

                        wtRow++;
                    }

                    sh.Columns.AutoFit();
                    //保存
                    await package.SaveAsync();
                    return package.File.FullName;
                }
            });
            return t;
        }

        /// <summary>
        /// 计算编辑距离
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++)
            {
            }

            for (int j = 0; j <= targetWordCount; distance[0, j] = j++)
            {
            }

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        /// <summary>
        /// 用编辑距离计算相似度
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        /// <summary>
        /// 获取Column
        /// </summary>
        /// <param name="findRange"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private static int MyGetCol(ExcelRange findRange, string keyWord)
        {
            var query = (from cell in findRange
                where (cell.Value?.ToString() ?? string.Empty).ToLower().Contains(keyWord)
                select cell);

            //没有查找记录
            if (!query.Any())
            {
                return 0;
            }
            else
            {
                return query.First().Start.Column;
            }
        }

        /// <summary>
        /// 弹窗选择一个文件夹，返回选择的文件夹路径
        /// </summary>
        /// <param name="title"></param>
        /// <param name="initPath"></param>
        /// <returns></returns>
        public static string GetFolder(string title = "请选择一个文件夹", string initPath = "")
        {
            FolderPicker dlg = new FolderPicker();
            dlg.Title = title;
            if (initPath == "")
            {
                dlg.InputPath = Environment.GetEnvironmentVariable("%USERPROFILE%\\Desktop");
            }
            else
            {
                if (Directory.Exists(initPath))
                {
                    dlg.InputPath = initPath;
                }
                else
                {
                    dlg.InputPath = Environment.GetEnvironmentVariable("%USERPROFILE%\\Desktop");
                }
            }

            if (dlg.ShowDialog(IntPtr.Zero) == true)
            {
                return dlg.ResultPath;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 弹框让用户选择文件，可以设置是否多选
        /// eg. "PDF(*.pdf)|*.pdf"
        /// </summary>
        /// <param name="fileFilter"></param>
        /// <param name="isMultiselect"></param>
        /// <param name="title"></param>
        /// <param name="initDir"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string fileFilter, bool isMultiselect, string title = "请选择文件", string initDir = "")
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = isMultiselect; //该值确定是否可以选择多个文件
            dialog.Title = title; //弹窗的标题

            //默认打开的文件夹的位置
            if (Directory.Exists(initDir))
            {
                dialog.InitialDirectory = initDir;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetEnvironmentVariable("%USERPROFILE%\\Desktop");
            }

            dialog.Filter = fileFilter; //筛选文件
            dialog.ShowHelp = false; //是否显示“帮助”按钮

            List<string> filePathList = new List<string>();

            DialogResult res = dialog.ShowDialog();

            if (res == DialogResult.OK || res == DialogResult.Yes)
            {
                foreach (string path in dialog.FileNames)
                {
                    filePathList.Add(path);
                }
            }

            return filePathList;
        }

        /// <summary>
        /// 判断文件是否被其他进程占用
        /// 人手打开了excel文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)

                //等1.5秒再判断一次
                Task.Delay(1500).Wait();
                try
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                }
                catch (IOException)
                {
                    //file is locked after 2 seconds
                    return true;
                }

                //file is not locked
                return false;
            }

            //file is not locked
            return false;
        }
    }


////////////////////////////////////////////////////////////////////


    public class OrderData
    {
        public string OrderNo;
        public string MaterialNo;
        public string CustomerItem;
        public string AveryItem;
        public int Qty;
        public double Price;
        public double UnitPrice;
        public string System;
        public string CompanyName;
        public string ParentFolderName;

        public string Sol;
    }

    public class ReportData
    {
        public string Po;
        public string Item;

        public int Qty;

        // (Order_Number)_(Line_Number)
        public string Sol;
    }
}