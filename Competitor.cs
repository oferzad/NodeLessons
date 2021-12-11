using System;
using System.Collections.Generic;
using System.Text;
using DataStructureCore;

namespace NodeLessons
{
    class Competitor
    {
        int minutes, seconds;
        string name;
        public Competitor(string name, int min, int sec)
        {
            this.name = name;
            this.minutes = min;
            this.seconds = sec;
        }
        public int GetMinutes() { return this.minutes; }
        public int GetSeconds() { return this.seconds; }
        public string GetName() { return this.name; }
        public bool IsFasterThan(Competitor c)
        {
            return (this.minutes < c.minutes ||
                (this.minutes == c.minutes && this.seconds < c.seconds)) ;
        }
    }

    class Race
    {
        Node<Competitor> comp;
        public Race()
        {
            this.comp = null;
        }
        public void Add(Competitor c)
        {
            //check if this is the first comp
            if (this.comp == null)
            {
                this.comp = new Node<Competitor>(c);
            }
            else //add in a sorted manner
            {
                Node<Competitor> pos = this.comp;
                //check if fastest
                Competitor current = pos.GetValue();
                if (c.IsFasterThan(current))
                {
                    this.comp = new Node<Competitor>(c, this.comp);
                }
                else //check the position to be placed
                {
                    while (pos.HasNext())
                    {
                        Node<Competitor> next = pos.GetNext();
                        current = next.GetValue();
                        if (c.IsFasterThan(current))
                        {
                            Node<Competitor> newNode = new Node<Competitor>(c, next);
                            pos.SetNext(newNode);
                            //we are done
                            return;
                        }
                        pos = pos.GetNext();
                    }
                    //if we got here, the new competitor is the slowest and should be added here
                    pos.SetNext(new Node<Competitor>(c));
                }
            }
        }
        public string Rank(int x)
        {
            Node<Competitor> pos = this.comp;
            int counter = 1;
            while(pos != null)
            {
                Competitor c = pos.GetValue();
                if (counter == x)
                    return c.GetName();

                counter++;
                pos = pos.GetNext();
            }
            return "No one!";
        }
    }
}
