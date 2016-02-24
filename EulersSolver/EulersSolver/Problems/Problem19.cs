using System;

namespace EulersSolver.Problems
{
    internal class Problem19 : BaseProblem
    {
        protected override int ProblemNumber => 19;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            You are given the following information, but you may prefer to do some research for yourself.

            1 Jan 1900 was a Monday.
            Thirty days has September,
            April, June and November.
            All the rest have thirty-one,
            Saving February alone,
            Which has twenty-eight, rain or shine.
            And on leap years, twenty-nine.
            A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
            How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
            */

            Initialize();
            // Doing it without DateTime doing the difficult stuff for me
            //int answer = CalculateAnswer();

            // Way better.. figures.
            int answer = NewAnswer();
            Finalize(answer);
        }

        private static int NewAnswer()
        {
            var sundaysOnFirstOfMonth = 0;

            DateTime datVar = new DateTime(1901, 1, 1);
            while (datVar.Year < 2001)
            {
                DebugLog(String.Format("{0,2}/01/{1,4} : {2}", datVar.Month, datVar.Year, datVar.DayOfWeek));
                if (datVar.DayOfWeek == DayOfWeek.Sunday)
                {
                    sundaysOnFirstOfMonth++;
                }
                datVar = datVar.AddMonths(1);
            }

            return sundaysOnFirstOfMonth;
        }

        private static int CalculateAnswer()
        {
            int day = 2, month = 1, year = 1901, sundaysOnTheFirstOfTheMonth = 0;

            var daysInNextMonth = GetDaysInMonth(month, year);
            while (year < 2001)
            {
                //LogDayOfWeekForFirstOfMonth(month, year, day);

                day = (day + daysInNextMonth) % 7;

                if (day == 0)
                {
                    sundaysOnTheFirstOfTheMonth++;
                }

                month++;

                if (month > 12)
                {
                    year++;
                    month = 1;
                }
            }

            return sundaysOnTheFirstOfTheMonth;
        }

        private static int GetDaysInMonth(int month, int year)
        {
            /*
            Thirty days has September,
            April, June and November.
            All the rest have thirty-one,
            Saving February alone,
            Which has twenty-eight, rain or shine.
            And on leap years, twenty-nine.
             */
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11: return 30;
                case 2: return CalculateDaysInFebruaryBasedOnYear(month, year);
                default: return 31;
            }
        }

        private static int CalculateDaysInFebruaryBasedOnYear(int month, int year)
        {
            /*
            Saving February alone,
            Which has twenty-eight, rain or shine.
            And on leap years, twenty-nine.
             */
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                return 29;
            }
            else
            {
                return 28;
            }
        }

        private static void LogDayOfWeekForFirstOfMonth(int month, int year, int day)
        {
            string dayName = Enum.GetName(typeof(DayOfWeek), day);
            DebugLog(String.Format("{0,2}/01/{1,4} : {2}", month, year, dayName));
        }
    }
}