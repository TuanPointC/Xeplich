using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xep_lich.Functions
{
    public class CheckConditions
    {
        private static int[] GetWeek(ClassInfo classInfor)
        {
            var result = new int[2];
            if (classInfor.Date_Pattern == "Full")
            {
                result = new int[2] { 1, 15 };
            }
            else
            {
                var subDatePattern = classInfor?.Date_Pattern?.Split(" ")[1];
                if (subDatePattern != null)
                {
                    result[0] = Convert.ToInt32(subDatePattern.Split("-")[0]);
                    result[1] = Convert.ToInt32(subDatePattern.Split("-")[1]);
                }
            }
            return result;
        }

        private static double[] GetHour(ClassInfo classInfor)
        {
            var result = new double[2];
            //get start hour
            var startHourString = classInfor.Start_Time;
            var hourStart = startHourString?[..^1].Split(":")[0];
            var minuteStart = startHourString?[0..^1].Split(":")[1];
            var typeStart = startHourString?[^1];
            if (typeStart != 'a' && hourStart!="12")
            {
                result[0] = Convert.ToDouble(hourStart) + 12 + Convert.ToDouble(minuteStart) / 60;

            }
            else
            {
                result[0] = Convert.ToDouble(hourStart) + Convert.ToDouble(minuteStart) / 60;
            }

            // get end hour
            var endHourString = classInfor.End_Time;
            var hourEnd = endHourString?[..^1].Split(":")[0];
            var minuteEnd = endHourString?[0..^1].Split(":")[1];
            var typeEnd = endHourString?[^1];
            if (typeStart != 'a' && hourStart != "12")
            {
                result[1] = Convert.ToDouble(hourEnd) + 12 + Convert.ToDouble(minuteEnd) / 60;
            }
            else
            {
                result[1] = Convert.ToDouble(hourEnd) + Convert.ToDouble(minuteEnd) / 60;
            }
            return result;
        }
        private static bool CheckTimeBetweenTwoClasses(ClassInfo class1, ClassInfo class2)
        {
            //Get week
            var weekOfClass1 = GetWeek(class1);
            var weekOfClass2 = GetWeek(class2);

            // Get hour
            var hourOfClass1 = GetHour(class1);
            var hourOfClass2 = GetHour(class2);

            // if 2 classes is not same week
            if (weekOfClass1[0] >= weekOfClass2[1] || weekOfClass1[1] <= weekOfClass2[0])
            {
                return true;
            }

            // if 2 classes is not same day
            if (class1.Day != class2.Day)
            {
                return true;
            }

            // check hour
            if (hourOfClass1[0] >= hourOfClass2[1] || hourOfClass1[1] <= hourOfClass2[0])
            {
                return true;
            }

            Console.WriteLine($"2 lop vi pham Li Thuyet: {class1.Course} -{class1.Itype} {class1.Section} {hourOfClass1[0]}:{hourOfClass1[1]} ____{class2.Course} -{class2.Itype} {class2.Section} {hourOfClass2[0]}:{hourOfClass2[1]}");
            return false;
        }

        private static bool CheckTheoryAndPractice(IEnumerable<ClassInfo> sol, ClassInfo newClass)
        {
            var theoryClass = sol.FirstOrDefault(c => c.Course == newClass.Course);
            if (theoryClass?.Section == 1)
            {
                if (newClass?.Section == 1 || newClass?.Section == 2)
                {
                    return true;
                }
            }
            else
            {
                if (newClass?.Section == 3 || newClass?.Section == 4)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckAll(IEnumerable<ClassInfo> sol, ClassInfo newClass)
        {
            if (newClass.Itype == "TH")
            {
                if (!CheckTheoryAndPractice(sol, newClass))
                {
                    return false;
                }
            }
            foreach (var classChild in sol)
            {
                if (!CheckTimeBetweenTwoClasses(classChild, newClass))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
