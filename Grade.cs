using System;
using System.Collections.Generic;
using System.Text;
using DataStructureCore;

namespace NodeLessons
{
    class Grade
    {
        private int score;
        private string subject;
        public Grade(string subject, int score)
        {
            this.score = score;
            this.subject = subject;
        }
        public int GetScore() { return this.score; }
        public string GetSubject() { return this.subject; }
        public void SetScore(int score) { this.score = score; }
        public void SetSubject(string subject) { this.subject = subject; }

        public override string ToString()
        {
            return $"({this.subject}, {this.score})";
        }
    }
    class Student
    {
        private string name;
        private Node<Grade> grades, lastGrade;

        public Student(string name)
        {
            this.name = name;
            this.grades = null;
            this.lastGrade = null;
        }

        public int Avg()
        {
            if (this.grades == null)
                return 0;
            int sum = 0, count = 0;
            Node<Grade> pos = this.grades;
            while (pos != null)
            {
                Grade g = pos.GetValue();
                sum += g.GetScore();
                count++;

                pos = pos.GetNext();
            }

            return sum / count;

        }

        public void AddGrade(string subject, int score)
        {
            Grade g = new Grade(subject, score);
            //בדיקה האם ציון ראשון
            if (this.grades == null)
            {
                this.grades = new Node<Grade>(g);
                this.lastGrade = this.grades;
            }
            else
            {
                this.lastGrade.SetNext(new Node<Grade>(g));
                this.lastGrade = this.lastGrade.GetNext();
            }
        }

        public Grade DeleteGrade(string subject)
        {
            Node<Grade> temp = this.grades;
            if (temp == null)
                return null;
            //בדיקה האם ראשון
            Grade g = temp.GetValue();
            if (g.GetSubject() == subject)
            {
                //האם יש רק חוליה אחת
                if (!temp.HasNext())
                {
                    this.grades = null;
                    this.lastGrade = null; 
                }
                else
                {
                    this.grades = this.grades.GetNext();
                    temp.SetNext(null);
                }
                return g;
            }
                
            //seach for subject in the list
            while (temp.HasNext())
            {
                Node<Grade> next = temp.GetNext();
                g = next.GetValue();
                if (g.GetSubject() == subject)
                {
                    temp.SetNext(next.GetNext());
                    next.SetNext(null);
                    //check if lastGrade should be updated
                    if (this.lastGrade == next)
                        this.lastGrade = temp;
                    return g;
                }

                temp = temp.GetNext();
            }
            //subject was not found so return null;
            return null;
        }

        public Grade DeleteGradeWithDummy(string subject)
        {
            if (this.grades == null)
                return null;

            Node<Grade> dummy = new Node<Grade>(null, this.grades);

            Node<Grade> temp = dummy;
            
            //seach for subject in the list
            while (temp.HasNext())
            {
                Node<Grade> next = temp.GetNext();
                Grade g = next.GetValue();
                if (g.GetSubject() == subject)
                {
                    temp.SetNext(next.GetNext());
                    next.SetNext(null);
                    //check if lastGrade should be updated
                    if (this.lastGrade == next)
                        this.lastGrade = temp;
                    //Update lastGrade again if we just deleted only node in the list
                    if (lastGrade == dummy)
                        lastGrade = null;
                    //Update grades in case we deleted the first node
                    this.grades = dummy.GetNext();
                    return g;
                }

                temp = temp.GetNext();
            }
            //subject was not found so return null;
            return null;
        }

        public void UpdateGrade(string subject, int score)
        {
            Node<Grade> temp = this.grades;
            while (temp != null)
            {
                Grade g = temp.GetValue();
                if (g.GetSubject() == subject)
                    g.SetScore(score);
                temp = temp.GetNext();
            }
        }
        public override string ToString()
        {
            return this.grades.ToString();
        }
    }
}
