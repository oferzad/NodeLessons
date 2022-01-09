using System;
using DataStructureCore;

namespace NodeLessons
{
    class Program
    {
        static Node<int> CreateFromUser()
        {
            const int ZAKIF = 0;
            Node<int> list = null, last=null;

            //קלט ראשון
            Console.WriteLine("Please Type a number or zero to end");
            int num = int.Parse(Console.ReadLine());

            //לולאת קליטה
            while (num != ZAKIF)
            {
                //טיפול בקלט
                if (list == null) //לוליה ראשונה
                {
                    list = new Node<int>(num);
                    last = list;
                }
                else
                {
                    last.SetNext(new Node<int>(num));
                    last = last.GetNext();
                }

                //קלט נוסף
                num = int.Parse(Console.ReadLine());
            }

            return list;
        }
        static Node<int> CreateList(int min, int max, int length)
        {
            Random r = new Random();
            Node<int> list = null;
            for (int i = 0; i < length; i++)
            {
                list = new Node<int>(r.Next(min, max + 1), list);
            }
            return list;
        }

        

        static int FindMaxSequence(Node<int> list)
        {
            if (list == null)
                return 0;

            //הגדרת משתנים
            int maxCount = 1, count = 1;
            Node<int> pos = list;

            //לולאת סריקה
            while(pos.HasNext())
            {
                Node<int> next = pos.GetNext();

                //טיפול
                if (pos.GetValue() + 1 == next.GetValue())
                {
                    count++;
                    if (count > maxCount)
                        maxCount = count;
                }
                else
                {
                    count = 1;
                }

                //move to next node!
                pos = pos.GetNext();
            }

            return maxCount;
        }

        static int Sum(Node<int> list)
        {
            int sum = 0;
            Node<int> pos = list;
            while(pos!=null)
            {
                sum += pos.GetValue();
                pos = pos.GetNext();
            }
            return sum;
        }

        static int SumRecursive(Node<int> list)
        {
            if (list == null)
                return 0;
            return list.GetValue() + SumRecursive(list.GetNext());
        }

        static Node<int> CreateRecursiveByUser()
        {
            const int ZAKIF = -1;
            int num = int.Parse(Console.ReadLine());

            //תנאי עצירה
            if (num == ZAKIF)
                return null;

            //תנאי כללי
            Node<int> list = new Node<int>(num);
            list.SetNext(CreateRecursiveByUser());
            return list;
        }

        static Node<int> CreateSumList(Node<int> list)
        {
            Node<int> pos = list;
            Node<int> newList = null, last = null;
            int sum = 0;
            const int ZAKIF = 0; //מסמן סוף תת שרשרת

            //התחלת סריקה
            while (pos != null)
            {
                //  טיפול בחוליה
                if (pos.GetValue() != ZAKIF)
                {
                    sum += pos.GetValue();
                }
                else
                {
                    //בניית חוליה חדשה בשרשרת החדשה
                    if (newList == null)
                    {
                        newList = new Node<int>(sum);
                        last = newList;
                    }
                    else
                    {
                        last.SetNext(new Node<int>(sum));
                        last = last.GetNext();
                    }
                    sum = 0;
                }

                //מעבר לחוליה הבאה
                pos = pos.GetNext();
            }

            return newList;
        }

        static Node<int> CreateSumListWithDummy(Node<int> list)
        {
            Node<int> pos = list;
            //ניצור את החוליה הסתמית ועדכן אותה כחוליה אחרונה
            Node<int> dummy = new Node<int>(0), last = dummy;
            int sum = 0;
            const int ZAKIF = 0; //מסמן סוף תת שרשרת

            //התחלת סריקה
            while (pos != null)
            {
                //  טיפול בחוליה
                if (pos.GetValue() != ZAKIF)
                {
                    sum += pos.GetValue();
                }
                else
                {
                    //בניית חוליה חדשה בשרשרת החדשה
                    last.SetNext(new Node<int>(sum));
                    last = last.GetNext();
                    sum = 0;
                }

                //מעבר לחוליה הבאה
                pos = pos.GetNext();
            }

            return dummy.GetNext();
        }

        static Node<int> CreateSumListRecursive(Node<int> list)
        {
            if (list.GetValue() == 0 && !list.HasNext())
                return new Node<int>(0);
            Node<int> temp = CreateSumListRecursive(list.GetNext());
            temp.SetValue(temp.GetValue() + list.GetValue());
            if (list.GetValue() == 0)
            {
                temp = new Node<int>(0, temp);
            }
            return temp;
        }

        //הוסף איברים עוקבים בין כל שתי חוליות עוקבות עולות 
        static void UpdateSequence(Node<int> list)
        {
            Node<int> pos = list;

            //סריקה עד האחרון
            while(pos.HasNext())
            {
                //הגדרת הפניה לעוקב
                Node<int> next = pos.GetNext();
                //טיפול בחוליה
                if (pos.GetValue() + 1 < next.GetValue())
                {
                    Node<int> newNode = new Node<int>(pos.GetValue() + 1, next);
                    pos.SetNext(newNode);
                }
                //מעבר לחוליה הבאה
                pos = pos.GetNext();
            }
        }

        //הוסף חוליה לשרשרת אחרי כל תת סדרה עולה עם מספר החוליות בתת סדרה 
        static void UpdateSequenceLength(Node<int> list)
        {

            Node<int> pos = list;
            int counter = 1;
            int numSeq = 0;
            //סריקה עד האחרון
            while (pos.HasNext())
            {
                //הגדרת הפניה לעוקב
                Node<int> next = pos.GetNext();
                //טיפול בחוליה
                if (pos.GetValue() >= next.GetValue())
                {
                    numSeq++;
                    Node<int> newNode = new Node<int>(counter, next);
                    pos.SetNext(newNode);
                    counter = 1;
                }
                else
                    counter++;

                //מעבר לחוליה הבאה
                pos = next;
            }
            //טיפול בסדרה האחרונה
            pos.SetNext(new Node<int>(counter));
            pos = pos.GetNext();
            //הוספה של מספר הרצפים החוליה אחרונה
            pos.SetNext(new Node<int>(numSeq));

        }

        //הוסף חוליה לשרשרת לפני כל תת סדרה עולה עם מספר החוליות בתת סדרה 
        static Node<int> UpdateSequenceLengthBefore(Node<int> list)
        {

            Node<int> first = new Node<int>(0, list), before = first;
            if (list == null)
                return before;

            Node<int> pos = list;
            int counter = 1;
            //Check first sequence
            while (pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                if (pos.GetValue() < next.GetValue())
                    counter++;
                else
                {
                    before.SetValue(counter);
                    counter = 1;
                    before = new Node<int>(1, next);
                    pos.SetNext(before);
                }
                pos = next;
            }
            //last sequence
            before.SetValue(counter);
            return first;
        }

        static Node<int> Fib(int n)
        {
            //תנאי עצירה
            if (n == 0)
                return null;
            if (n == 1)
                return new Node<int>(1);
            if (n == 2)
                return new Node<int>(1, new Node<int>(1));
            //נבנה שרשרת של חוליה אחת פחות
            Node<int> list = Fib(n - 1); //1 -> 1 -> null
            //הוספת חוליה אחרונה
            Node<int> last = list;
            int a = 1;
            while (last.HasNext())
            {
                a = last.GetValue();
                last = last.GetNext();
            }
            last.SetNext(new Node<int>(a + last.GetValue()));
            return list;
        }
        
        static Node<T> Reverse<T>(Node<T> list)
        {
            //תנאי עצירה
            if (list == null || !list.HasNext())
                return list;
            //נשמור את החוליה השנייה
            Node<T> next = list.GetNext();
            //נהפוך את השרשרת החל מהחוליה השנייה
            Node<T> reverese = Reverse<T>(next);
            //נחבר את החוליה הראשונה לכלום
            list.SetNext(null);
            //נחבר את החוליה השניה לראשונה
            next.SetNext(list);
            
            return reverese;
        }

        static Node<T> DeleteNode<T>(Node<T> list, Node<T> p)
        {
            //מחיקה מראש השרשרת
            if (list == p)
            {
                list = p.GetNext();
                p.SetNext(null);
            }
            else //מחיקה מאמצע השרשרת
            {
                Node<T> pos = list;
                while (pos.HasNext())
                {
                    Node<T> next = pos.GetNext();
                    if (next == p)
                    {
                        pos.SetNext(p.GetNext());
                        p.SetNext(null);
                    }
                    pos = pos.GetNext();
                }
            }
            return list;
        }

        static Node<int> DeleteValues(Node<int> list, int val)
        {
            //האם השרשרת ריקה
            if (list == null)
                return null;

            Node<int> tobeDeleted;
            //מחיקת חוליה ראשונה
            while (list.GetValue() == val)
            {
                tobeDeleted = list;
                list = list.GetNext();
                tobeDeleted.SetNext(null);
            }

            if (list == null)
                return null;

            //מחיקה מאמצע השרשרת
            Node<int> pos = list;
            while (pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                if (next.GetValue() == val)
                {
                    pos.SetNext(next.GetNext());
                    next.SetNext(null);
                }
                else
                    pos = pos.GetNext();
            }

            return list;

        }

        static Node<int> DeleteValuesWithDummy(Node<int> list, int val)
        {
            //האם השרשרת ריקה
            if (list == null)
                return null;

            //נגדיר את חוליית הדמה
            Node<int> dummy = new Node<int>(val - 1, list);

            //מחיקה מאמצע השרשרת
            Node<int> pos = dummy;
            while (pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                if (next.GetValue() == val)
                {
                    pos.SetNext(next.GetNext());
                    next.SetNext(null);
                }
                else
                    pos = pos.GetNext();
            }

            return dummy.GetNext();
        }
        /*
         כתוב פעולה המקבלת שתי רשימות. נתון שהרשימה השניה מוכלת ברצף ברשימה הראשונה פעם אחת בלבד
        הפעולה תנתק את השרשרת הראשונה מהשניה.
         */
        static Node<int> DeleteLst(Node<int> lst1, Node<int> lst2)
        {
            Node<int> dummy = new Node<int>(0, lst1);
            Node<int> pos = dummy;
            while (pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                Node<int> last = FindLast(next, lst2);
                if (last != null)
                {
                    pos.SetNext(last.GetNext());
                    last.SetNext(null);
                }
                else
                    pos = pos.GetNext();
            }
            return dummy.GetNext();
        }
        static Node<int> FindLast(Node<int> lst1, Node<int> lst2)
        {
            return null;
        }

        static Node<T> flipList<T>(Node<T> list)
        {
            Node<T> dummy = flipList<T>(list, new Node<T>(list.GetValue()));
            return dummy.GetNext();
        }
        static Node<T> flipList<T>(Node<T> list, Node<T> flippedList)
        {
            if (list == null)
                return flippedList;
            if (!list.HasNext())
            {
                flippedList.SetNext(list);
                return flippedList;
            }
            Node<T> last = removeLastNode(list);
            flippedList.SetNext(last);
            flippedList.SetNext(flipList(list, flippedList.GetNext()));
            return flippedList;
        }
        static Node<T> removeLastNode<T>(Node<T> list)// פעולה מקבלת רישמה מנתקת את הערך האחרון שלה ומחזירה אותו
        {
            Node<T> pos = list;
            if (!list.HasNext())
                return list;
            while (pos.HasNext())
            {
                pos = pos.GetNext();
            }
            Node<T> pos2 = list;
            while (pos2.GetNext().HasNext())
            {
                pos2 = pos2.GetNext();
            }
            pos2.SetNext(null);
            return pos;
        }

        //פעולת עזר לתרגיל 2 בדפים
        //הפעולה מקבלת חוליה ומחזירה הפניה לחוליה אחרונה ברצף של יותר מ 2 חוליות
        //עם אותו ערך החל בחוליה העוקבת לזו שהתקבלה
        //או נאל אם אין רצף כזה
        static Node<int> FD(Node<int> lst)
        {
            if (lst == null || !lst.HasNext())
                return null;
            int val = lst.GetNext().GetValue();
            int counter = 0;
            Node<int> last = lst;
            while(last.HasNext() && last.GetNext().GetValue() == val)
            {
                counter++;
                last = last.GetNext();
            }

            if (counter <= 2)
                return null;
            return last;
        }
        //תרגיל 2 מהדפים שמוחק כל רצף של יותר משני איברים זהים
        static Node<int> Exc2(Node<int> lst)
        {
            Node<int> dummy = new Node<int>(0, lst);
            Node<int> pos = dummy;
            while(pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                Node<int> last = FD(pos);
                if (last != null)
                {
                    pos.SetNext(last.GetNext());
                    last.SetNext(null);
                }
                else
                    pos = pos.GetNext();
            }
            return dummy.GetNext();
        }
        //פעולה שמוצאת מרכז של שרשרת חוליות 
        static Node<int> Middle(Node<int> lst)
        {
            Node<int> fast = lst, slow = lst;
            while (fast != null)
            {
                fast = fast.GetNext();
                if (fast != null)
                {
                    fast = fast.GetNext();
                    slow = slow.GetNext();
                }
            }
            return slow;
        }

        static bool IsLoop(Node<int> lst)
        {
            Node<int> fast = lst, slow = lst;
            while (fast != null)
            {
                fast = fast.GetNext();
                if (fast != null)
                {
                    fast = fast.GetNext();
                    slow = slow.GetNext();
                }
                if (fast == slow)
                    return true;
            }
            return false;
        }

        static Node<int> Sod(Node<int> lst)
        {
            if (lst == null || !lst.HasNext())
                return lst;
            Node<int> next = lst.GetNext();
            if (lst.GetValue() == next.GetValue())
            {
                lst.SetNext(null);
                return Sod(next);
            }
            lst.SetNext(Sod(next));
            return lst;
        }

        static Node<T> Split<T>(Node<T> lst)
        {
            if (lst == null || !lst.HasNext())
                return null;
            Node<T> pos = lst, ret = lst.GetNext();
            while(pos.HasNext())
            {
                Node<T> next = pos.GetNext();
                pos.SetNext(next.GetNext());
                pos = next;
            }
            return ret;
        }

        static Node<T> Shuffle<T>(Node<T> lst1, Node<T> lst2)
        {
            Node<T> pos1 = lst1, pos2 = lst2;
            if (pos1 == null)
                return lst2;
            while(pos1 != null && pos2 != null)
            {
                Node<T> next = pos1.GetNext();
                pos1.SetNext(pos2);
                pos1 = pos2;
                pos2 = next;
            }
            return lst1;
        }

        static Node<T> ShuffleRecursive<T>(Node<T> lst1, Node<T> lst2)
        {
            if (lst1 == null)
                return lst2;
            if (lst2 == null)
                return lst1;

            Node<T> temp = ShuffleRecursive<T>(lst1.GetNext(), lst2.GetNext());
            lst1.SetNext(lst2);
            lst2.SetNext(temp);
            return lst1;
        }
        static Random r = new Random();
        static string[] subjects = { "Math", "Computer Sciense", "English", "Web Services", "Phisics", "Chemistry", "Sport" };
        static Grade GetRandomGrade()
        {
            return new Grade(subjects[r.Next(0, subjects.Length)], r.Next(40, 101));
        }
        static Node<Grade> CreateGradeList()
        {
            
            Node<Grade> lst = null;
            for (int i = 0; i < subjects.Length; i++)
            {
                Grade g = new Grade(subjects[i], r.Next(40, 101));
                lst = new Node<Grade>(g, lst);
            }
            return lst;
        }
        
        static void PrintFailures()
        {
            Node<Grade> grades = CreateGradeList();
            Console.WriteLine(grades);

            int counter = 0;
            Node<Grade> pos = grades;
            while (pos != null)
            {
                Grade g = pos.GetValue();
                if (g.GetScore() < 60)
                    counter++;

                pos = pos.GetNext();
            }
            Console.WriteLine(counter);

        }

        static void DeleteFailures()
        {
            Node<Grade> grades = CreateGradeList();
            Console.WriteLine(grades);

            Node<Grade> dummy = new Node<Grade>(null, grades);
            Node<Grade> pos = dummy;
            while (pos.HasNext())
            {
                Node<Grade> next = pos.GetNext();
                Grade g = next.GetValue();
                if (g.GetScore() < 60)
                {
                    pos.SetNext(next.GetNext());
                    next.SetNext(null);
                }
                else
                    pos = pos.GetNext();
            }
            grades = dummy.GetNext();
            Console.WriteLine(grades);
        }

        static void HomeWorkDec10()
        {
            Student st = new Student("ofer");
            for (int i = 0; i < 20; i++)
            {
                Grade gg = GetRandomGrade();
                st.AddGrade(gg.GetSubject(), gg.GetScore());
            }
            
            Console.WriteLine(st);

            while (st.DeleteGrade("Math") != null) { }
            
            
            st.UpdateGrade("Computer Sciense", 100);

            Console.WriteLine(st);
        }

        static Node<int> Bagrut2020Special(Node<int> lst)
        {
            //scan with pos
            Node<int> pos = lst;
            //create the new list with dummy first
            Node<int> dummy = new Node<int>(0), last = dummy;

            //loop through the list until the end
            while (pos != null)
            {
                int num = pos.GetValue();
                //break all digits of num 
                while (num > 0)
                {
                    //add new node to the end of the list
                    last.SetNext(new Node<int>(num % 10));
                    last = last.GetNext();
                    num = num / 10;
                }
                //add -9 on the end of the chain of digits
                last.SetNext(new Node<int>(-9));
                last = last.GetNext();

                //move to the next number!
                pos = pos.GetNext();
            }

            //return the created list (without dummy!!)
            return dummy.GetNext();
        }

        //create a list that reverse every upward sequence of numbers while maintaining the order of the sequences
        //for example:
        //1,2,3,2,3,4,5,3,4,6 will return:
        //3,2,1,5,4,3,2,6,4,3
        static Node<int> CreateReverse(Node<int> lst)
        {
            //empty list return null...
            if (lst == null)
                return null;
            //create the new list using dummy. start - always point to node before the start of a new sequence in the new list
            //last - point to the end of the new list
            Node<int> dummy = new Node<int>(0), start = dummy;
            //first node in the new list is always the first node from the original one
            dummy.SetNext(new Node<int>(lst.GetValue()));
            Node<int> last = dummy.GetNext();
            //loop through the list
            Node<int> pos = lst; 
            while (pos.HasNext())
            {
                Node<int> next = pos.GetNext();
                //create the new node (later we will connect it
                Node<int> newNode = new Node<int>(next.GetValue());
                //check if a sequence is finished! in such case, add to the end and update start
                if(pos.GetValue() >= next.GetValue())
                {
                    last.SetNext(newNode);
                    start = last;
                    last = newNode;
                }
                else //we are still in a sequence to add to the begining of the sequence
                {
                    newNode.SetNext(start.GetNext());
                    start.SetNext(newNode);
                }
                pos = pos.GetNext();
            }
            return dummy.GetNext();
            
        }

        //******* this is q num 4 fro the pages
        static bool Search(Node<int> lst, int val)
        {
            while (lst != null)
            {
                if (lst.GetValue() == val)
                    return true;
                lst = lst.GetNext();
            }
            return false;
        }
        static Node<int> Exc4(Node<int> lst, int num)
        {
            Node<int> dummy = new Node<int>(0), last = dummy;
            for (int i = 0; i < num; i++)
            {
                if (!Search(lst, i))
                {
                    last.SetNext(new Node<int>(i));
                    last = last.GetNext();
                }
            }
            return dummy.GetNext();

        }

        static void FillList(Node<int> lst)
        {
            Node<int> pos = lst;
            while(pos.HasNext())
            {
                Node<int> next = pos.GetNext();

                //טיפול בחוליה
                Node<int> newNode;
                if (pos.GetValue() + 1 < next.GetValue())
                {
                    newNode = new Node<int>(pos.GetValue() + 1, next);
                    pos.SetNext(newNode);
                }
                if (pos.GetValue() - 1 > next.GetValue())
                {
                    newNode = new Node<int>(pos.GetValue() - 1, next);
                    pos.SetNext(newNode);
                }

                pos = pos.GetNext();
            }
        }

        //excersize from test reharsle
        static Node<int> Groups(Node<int> list)
        {
            if (list == null)
                return new Node<int>(0);

            Node<int> temp = list;
            int counter = 0;

            while (temp != null)
            {
                counter++;
                int val = temp.GetValue();
                Node<int> deleted = DeleteFirstOccurance(temp, val);
                while (deleted != null)
                {
                    deleted.SetNext(temp.GetNext());
                    temp.SetNext(deleted);
                    temp = deleted;
                    deleted = DeleteFirstOccurance(temp, val);
                }
                if (!temp.HasNext())
                {
                    temp.SetNext(new Node<int>(counter));
                    temp = null;
                }
                else
                    temp = temp.GetNext();
                
            }
            return list;
        }

        static Node<int> DeleteFirstOccurance(Node<int> list, int val)
        {
            if (list == null)
                return null;
            Node<int> temp = list;
            while (temp.HasNext())
            {
                Node<int> next = temp.GetNext();
                if (next.GetValue() == val)
                {
                    temp.SetNext(next.GetNext());
                    next.SetNext(null);
                    return next;
                }
                temp = temp.GetNext();
            }
            return null;
        }

        static void Main(string[] args)
        {
            Node<int> list = new Node<int>(0);
            list = new Node<int>(3, list);
            list = new Node<int>(4, list);
            list = new Node<int>(0, list);
            list = new Node<int>(3, list);
            list = new Node<int>(4, list);
            list = new Node<int>(5, list);
            Node<int> created = CreateSumListRecursive(list);
            Console.WriteLine(list);
            Console.WriteLine(created);
            //DynamicArray<int> arr = new DynamicArray<int>(10);
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = i;
            //}
            //Console.WriteLine("Resize to 15");
            //arr.Resize(15);
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine($"arr[{i}]={arr[i]}");
            //}
            //Console.WriteLine("Resize to 5");
            //arr.Resize(5);
            //for (int i = 0; i < arr.GetLength(); i++)
            //{
            //    Console.WriteLine($"arr[{i}]={arr[i]}");
            //}

            //Node<int> lst = CreateList(0, 100, 10);
            //Node<int> lst2 = CreateReverse(lst);
            //Console.WriteLine(lst);
            //Console.WriteLine(lst2);
        }
    }
}
