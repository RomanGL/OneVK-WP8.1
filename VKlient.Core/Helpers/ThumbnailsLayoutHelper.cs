﻿using OneVK.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет класс-помощник для работы с миниатюрами изображений.
    /// </summary>
    public static class ThumbnailsLayoutHelper
    {
        /// <summary>
        /// Рассчитать размеры указынных миниатюр для заполнения указанной
        /// прямоугольной области.
        /// </summary>
        /// <param name="maxContainerSize">Максимальный размер прямоугольной области,
        /// отведенной для указанных миниатюр.</param>
        /// <param name="thumbnails">Коллекция миниатюр для рассчета.</param>
        /// <param name="marginBetween">Отступ между миниатюрами.</param>
        public static void CalculateThumbnailSizes(Rectangle maxContainerSize,
            IList<IThumbnailSupport> thumbnails, double marginBetween)
        {
            string str = String.Empty;
            int count = thumbnails.Count;
            var numArray = new int[3];
            var ratios = new double[count];            
            bool flag = false;

            for (int i = 0; i < count; i++)
            {
                double ratio = thumbnails[i].GetRatio();
                if (ratio == -1.0) flag = true;
                char orient = ratio > 1.2 ? 'w' : (ratio < 0.8 ? 'n' : 'q');
                str += orient;
                numArray[OrientationToInt(orient)]++;
                ratios[i] = ratio;
            }

            if (flag) return;

            double num1 = ratios.Length > 0 ? ratios.Sum() / (double)ratios.Length : 1;
            double num2 = marginBetween;
            double width1 = maxContainerSize.Width;
            double num3 = maxContainerSize.Height;
            double num4 = width1 / num3;

            if (count == 1)
            {
                if (ratios[0] > 0.8)
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width1,
                        Height = width1 / ratios[0]
                    };
                else
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = num3 * ratios[0],
                        Height= num3
                    };
            }
            else if (count == 2)
            {
                if (str == "ww" && num1 > 1.4 * num4 && ratios[1] - ratios[0] < 0.2)
                {
                    double height = Math.Min(width1 / ratios[0], Math.Min(width1 / ratios[1], (num3 - num2) / 2.0));
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width1,
                        Height = height,
                        LastColumn = true
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width1,
                        Height = height
                    };
                }
                else if (str == "ww" || str == "qq")
                {
                    double width2 = (width1 - marginBetween) / 2.0;
                    double height = Math.Min(width2 / ratios[0], Math.Min(width2 / ratios[1], num3));
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height,
                        LastColumn = true
                    };
                }
                else
                {
                    double width2 = (width1 - marginBetween) / ratios[1] / (1.0 / ratios[0] + 1.0 / ratios[1]);
                    double width3 = width1 - width2 - marginBetween;
                    double height = Math.Min(num3, Math.Min(width2 / ratios[0], width3 / ratios[1]));
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height
                    };
                }
            }
            else if (count == 3)
            {
                if (str == "www")
                {
                    double height1 = Math.Min(width1 / ratios[0], (num3 - num2) * 0.66);
                    double width2 = (width1 - marginBetween) / 2.0;
                    double height2 = Math.Min(num3 - height1 - num2, Math.Min(width2 / ratios[1], width2 / ratios[2]));
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width1,
                        Height = height1,
                        LastColumn = true
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height2
                    };
                    thumbnails[2].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height2
                    };
                }
                else
                {
                    double height1 = num3;                    
                    double height2 = ratios[1] * (num3 - num2) / (ratios[2] + ratios[1]);
                    double height3 = num3 - height2 - num2;
                    double width2 = Math.Min(height1 * ratios[0], (width1 - marginBetween) * 0.75);
                    double width3 = Math.Min(width1 - width2 - marginBetween, 
                        Math.Min(height2 * ratios[2], height3 * ratios[1]));
                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height1
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height3,
                        LastRow = true
                    };
                    thumbnails[2].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height3,
                        LastRow = true
                    };
                }
            }
            else if (count == 4)
            {
                if (str == "wwww")
                {
                    double height1 = Math.Min(width1 / ratios[0], (num3 - num2) * 0.66);
                    double val2 = (width1 - 2.0 * marginBetween) / (ratios[1] + ratios[2] + ratios[3]);
                    double height2 = Math.Min(num3 - height1 - num2, val2);
                    double width2 = val2 * ratios[1];
                    double width3 = val2 * ratios[2];
                    double width4 = val2 * ratios[3];

                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width1,
                        Height = height1,
                        LastColumn = true
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height2
                    };
                    thumbnails[2].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height2
                    };
                    thumbnails[3].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width4,
                        Height = height2,
                        LastColumn = true
                    };
                }
                else
                {
                    double height1 = num3;
                    double width2 = Math.Min(height1 * ratios[0], (width1 - marginBetween) * 0.66);
                    double val2 = (num3 - 2.0 * num2) / (1.0 / ratios[1] + 1.0 / ratios[2] + 1.0 / ratios[3]);
                    double height2 = val2 / ratios[1];
                    double height3 = val2 / ratios[2];
                    double height4 = val2 / ratios[3];
                    double width3 = Math.Min(width1 - width2 - marginBetween, val2);

                    thumbnails[0].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width2,
                        Height = height1
                    };
                    thumbnails[1].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height2,
                        LastRow = true
                    };
                    thumbnails[2].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height3,
                        LastRow = true
                    };
                    thumbnails[3].ThumbnailSize = new ThumbnailSize
                    {
                        Width = width3,
                        Height = height4,
                        LastRow = true
                    };
                }
            }
            else
            {
                var list1 = new double[count];
                if (num1 > 1.1)
                    for (int i = 0; i < count; i++)
                        list1[i] = Math.Max(1.0, ratios[i]);
                else
                    for (int i = 0; i < count; i++)
                        list1[i] = Math.Min(1.0, ratios[i]);
                var dictionary = new Dictionary<string, double[]>();
                int num5 = count;

                dictionary[string.Concat(num5)] = new double[]
                {
                    CalculateMultiThumbnailsHeight(list1, width1, marginBetween)
                };

                for (int i = 0; i <= count - 1; i++)
                {
                    int num6 = count - i;
                    dictionary[i + "," + num6] = new double[]
                    {
                        CalculateMultiThumbnailsHeight(list1.GetRange(0, i), width1, marginBetween),
                        CalculateMultiThumbnailsHeight(list1.GetRange(i, list1.Length), width1, marginBetween)
                    };
                }

                for (int i = 0; i <= count - 2; i++)
                    for (int j = 0; j <= count - i - 1; j++)
                        dictionary[i + "," + j + "," + (count - i - j)] = new double[]
                        {
                            CalculateMultiThumbnailsHeight(list1.GetRange(0, i), width1, marginBetween),
                            CalculateMultiThumbnailsHeight(list1.GetRange(i, i + j), width1, marginBetween),
                            CalculateMultiThumbnailsHeight(list1.GetRange(i + j, list1.Length), width1, marginBetween)
                        };

                string index = null;
                double num7 = 0;

                foreach (string index1 in dictionary.Keys)
                {
                    var list2 = dictionary[index1];
                    double num6 = num2 * (list2.Length - 1);
                    for (int i = 0; i < list2.Length; i++)
                        num6 += list2[i];
                    double num9 = Math.Abs(num6 - num3);

                    if (index1.IndexOf(",") != -1)
                    {
                        string[] strArray = index1.Split(',');
                        if (int.Parse(strArray[0]) > int.Parse(strArray[1]) ||
                            (strArray.Length > 2 && int.Parse(strArray[1]) > int.Parse(strArray[2])))
                            num9 *= 1.1;
                    }
                    if (index == null || num9 < num7)
                    {
                        index = index1;
                        num7 = num9;
                    }
                }
                
                var thumbs = new List<IThumbnailSupport>(thumbnails);
                var list3 = new List<double>(list1);
                var list4 = dictionary[index];
                var strArray1 = index.Split(',');
                int index2 = 0;

                for (int i = 0; i < strArray1.Length; i++)
                {
                    int num6 = int.Parse(strArray1[i]);
                    var thumbs1 = new List<IThumbnailSupport>();
                    for (int j = 0; j < num6; j++)
                    {
                        thumbs1.Add(thumbs[0]);
                        thumbs.RemoveAt(0);
                    }

                    double height = list4[index2];
                    index2++;
                    int num8 = thumbs1.Count - 1;

                    for (int j = 0; j < thumbs1.Count; j++)
                    {
                        double num9 = list3[0];
                        list3.RemoveAt(0);
                        thumbs1[j].ThumbnailSize = new ThumbnailSize
                        {
                            Width = num9 * height,
                            Height = height,
                            LastColumn = j == num8
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Рассчитывает высоту для некольких миниатюр.
        /// </summary>
        /// <param name="ratios">Коллекция соотношений сторон.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="margin">отступ между элементами.</param>
        private static double CalculateMultiThumbnailsHeight(double[] ratios, double width, double margin)
        {
            return (double)(width - (ratios.Length - 1) * margin) / ratios.Sum();
        }

        /// <summary>
        /// Возвращает числовое значение исходя
        /// </summary>
        /// <param name="orient">Символьное обозначение ориентации.</param>
        private static int OrientationToInt(char orient)
        {
            switch (orient)
            {
                case 'w':
                    return 0;
                case 'n':
                    return 1;
                case 'q':
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
