using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OsCoreApplication.Common
{
    public class ClsCommon
    {

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string RemoveSignsVietnamese(string str)
        {
            if (str.Length > 0)
            {
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }
            return str;
        }

        public static string RemoveSpace(string str)
        {
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }

        public static string CreateMetatitle(string title)
        {
            title = RemoveSignsVietnamese(title);
            title = RemoveSpace(title);
            title = title.Replace(" ", "-");
            title = title.Replace("&", "-");
            title = title.Replace("?", "-");
            title = title.Replace("(", "");
            title = title.Replace(")", "");
            title = title.Replace(":", "");
            title = title.Replace(",", "");
            title = title.Replace(".", "");
            title = title.Replace("!", "");
            title = title.Replace("/", "-");
            title = title.Replace("\\", "");
            return title.ToLower();
        }

            public static string ValidateRss(string title)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            title = rRemScript.Replace(title, "");
            return title;

        }
        


        public static string[] SetPage(int Page, int TotalRows, int PageSize)
        {
            int MaxPage = 0;
            string[] page = new string[] { "1", "", "", "", "", "", "" };

            if (TotalRows % PageSize == 0)
                MaxPage = TotalRows / PageSize;
            else
                MaxPage = TotalRows / PageSize + 1;
            page[6] = MaxPage.ToString();
            if (Page == 1)
            {
                for (int i = 1; i <= MaxPage; i++)
                {
                    if (i <= 5)
                    {
                        switch (i)
                        {
                            case 1: page[1] = i.ToString(); break;
                            case 2: page[2] = i.ToString(); break;
                            case 3: page[3] = i.ToString(); break;
                            case 4: page[4] = i.ToString(); break;
                            case 5: page[5] = i.ToString(); break;
                        }
                    }
                    else
                        break;
                }
            }
            else
            {
                if (Page == 2)
                {
                    for (int i = 1; i <= MaxPage; i++)
                    {
                        if (i == 1)
                            page[1] = "1";
                        if (i <= 5)
                        {
                            switch (i)
                            {
                                case 2: page[2] = i.ToString(); break;
                                case 3: page[3] = i.ToString(); break;
                                case 4: page[4] = i.ToString(); break;
                                case 5: page[5] = i.ToString(); break;
                            }
                        }
                        else
                            break;
                    }
                }
                else
                {
                    int Cout = 1;
                    if (Page <= MaxPage)
                    {
                        for (int i = Page; i <= MaxPage; i++)
                        {
                            if (i == Page)
                            {
                                page[1] = (Page - 2).ToString();
                                page[2] = (Page - 1).ToString();
                            }
                            if (Cout <= 3)
                            {
                                if (i == Page)
                                    page[3] = i.ToString();
                                if (i == (Page + 1))
                                    page[4] = i.ToString();
                                if (i == (Page + 2))
                                    page[5] = i.ToString();
                                Cout++;
                            }
                            else
                                break;
                        }
                    }
                    else
                    {
                        SetPage(Page, TotalRows, PageSize);
                    }
                }
            }
            return page;
        }
    }
}
