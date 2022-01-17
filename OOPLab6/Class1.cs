using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OOPLab6
{
    class Shape
    {
        protected Color clr;
        virtual public void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
        }
        virtual public bool isChecked(MouseEventArgs e)
        {
            return false;
        }
        virtual public bool getF()
        {
            return false;
        }
        virtual public void slctT()
        {
        }
        virtual public void slctF()
        {
        }
        virtual public void changeFig()
        {
        }
        virtual public void move(PictureBox sender, int _x, int _y)
        {
        }
        virtual public void resize(PictureBox sender, int _d)
        {
        }
        virtual public void changeColor(Color _clr)
        {
            clr = _clr;
        }
        virtual public void save(StreamWriter _file) //сохранение объекта в файл
        {
        }
        virtual public void load(StreamReader _file) //выгрузка данных об объекте из файла
        {
        }
        virtual public String ret()
        {
            return "";
        }
        virtual public Point getP()
        {
            Point p = new Point(0, 0);
            return p;
        }
    }
    class CCircle : Shape
    {
        private int x, y, r;
        private bool fl; //флаг выделения объекта
        public CCircle(int _x, int _y, int _r, Color _clr)
        {
            x = _x;
            y = _y;
            r = _r;
            fl = true;
            clr = _clr;
        }
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - r, y - r, r * 2, r * 2);
            Pen pen = new Pen(clr);
            if (fl == true)
            {
                pen.Width = 2; //выделение
            }
            g.DrawEllipse(pen, rect);
            sender.Image = bmp;
        }
        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            if (((e.X - x) * (e.X - x) + (e.Y - y) * (e.Y - y)) <= (r * r))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF()
        {
            return fl;
        }
        override public void slctT()
        {
            fl = true;
        }
        override public void slctF()
        {
            fl = false;
        }
        override public void move(PictureBox sender, int _x, int _y)
        {
            x = x + _x;
            y = y + _y;
            if (checkLocation(sender) == false)
            {
                x = x - _x;
                y = y - _y;
            }
        }
        override public void resize(PictureBox sender, int t)
        {
            r = r + t / 2;
            if (checkLocation(sender) == false)
            {
                r = r - t / 2;
            }
        }
        private bool checkLocation(PictureBox sender)
        {
            if ((x + r >= sender.Location.X + sender.Size.Width) || (x - r <= sender.Location.X) || (r < 1))
            {
                return false;
            }
            else if ((y + r >= sender.Location.Y + sender.Size.Height) || (y - r <= sender.Location.Y) || (r < 1))
            {
                return false;
            }
            return true;
        }
        override public void save(StreamWriter _file)
        {
            _file.WriteLine("C"); //запишем наименование сохраняемого объекта (круг)
            _file.WriteLine(x); //записываем его данные (координаты,радиус и цвет)
            _file.WriteLine(y);
            _file.WriteLine(r);
            _file.WriteLine(clr.ToKnownColor());
        }
        override public void load(StreamReader _file)
        {
            x = Convert.ToInt32(_file.ReadLine());
            y = Convert.ToInt32(_file.ReadLine());
            r = Convert.ToInt32(_file.ReadLine());
            clr = Color.FromName(_file.ReadLine());
        }
        override public String ret()
        {
            return "Круг";
        }
        override public Point getP()
        {
            Point p = new Point(x, y);
            return p;
        }
    }
    class CSquare : Shape
    {
        private int x, y, l;
        private bool fl;
        public CSquare(int _x, int _y, int _l, Color _clr)
        {
            x = _x;
            y = _y;
            l = _l;
            fl = true;
            clr = _clr;
        }
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - l / 2, y - l / 2, l, l);
            Pen pen = new Pen(clr);
            if (fl == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawRectangle(pen, rect);
            sender.Image = bmp;
        }
        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            if (e.X >= x - l / 2 && e.Y >= y - l / 2 && e.X <= x + l / 2 && e.Y <= y + l / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF()
        {
            return fl;
        }
        override public void slctT()
        {
            fl = true;
        }
        override public void slctF()
        {
            fl = false;
        }
        private bool checkLocation(PictureBox sender)
        {
            if ((x + l / 2 >= sender.Location.X + sender.Size.Width) || x - l / 2 <= sender.Location.X || l < 1)
            {
                return false;
            }
            else if ((y + l / 2 >= sender.Location.Y + sender.Size.Height) || y - l / 2 <= sender.Location.Y || l < 1)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)
        {
            x = x + _x;
            y = y + _y;
            if (checkLocation(sender) == false)
            {
                x = x - _x;
                y = y - _y;
            }
        }
        public override void resize(PictureBox sender, int _d)
        {
            l = l + _d / 2;
            if (checkLocation(sender) == false)
            {
                l = l - _d / 2;
            }
        }
        public override void save(StreamWriter _file)
        {
            _file.WriteLine("R");
            _file.WriteLine(x);
            _file.WriteLine(y);
            _file.WriteLine(l);
            _file.WriteLine(clr.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            x = Convert.ToInt32(_file.ReadLine());
            y = Convert.ToInt32(_file.ReadLine());
            l = Convert.ToInt32(_file.ReadLine());
            clr = Color.FromName(_file.ReadLine());
        }
        override public String ret()
        {
            return "Квадрат";
        }
        override public Point getP()
        {
            Point p = new Point(x, y);
            return p;
        }
    }
    class CTriangle : Shape
    {
        private Point a, b, c;
        private bool fl;
        public CTriangle(int x1, int y1, int x2, int y2, int x3, int y3, Color _clr)
        {
            a = new Point(x1, y1);
            b = new Point(x2, y2);
            c = new Point(x3, y3);
            fl = true;
            clr = _clr;
        }
        public override void save(StreamWriter _file)
        {
            _file.WriteLine("T");
            _file.WriteLine(a.X);
            _file.WriteLine(a.Y);
            _file.WriteLine(b.X);
            _file.WriteLine(b.Y);
            _file.WriteLine(c.X);
            _file.WriteLine(c.Y);
            _file.WriteLine(clr.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            a.X = Convert.ToInt32(_file.ReadLine());
            a.Y = Convert.ToInt32(_file.ReadLine());
            b.X = Convert.ToInt32(_file.ReadLine());
            b.Y = Convert.ToInt32(_file.ReadLine());
            c.X = Convert.ToInt32(_file.ReadLine());
            c.Y = Convert.ToInt32(_file.ReadLine());
            clr = Color.FromName(_file.ReadLine());
        }
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
            Pen pen = new Pen(clr);
            if (fl == true)
            {
                pen.Width = 2;
            }
            Point[] CurvePoints = { a, b, c };
            g.DrawPolygon(pen, CurvePoints);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender)
        {
            if ((b.X >= sender.Location.X + sender.Size.Width) || (c.X <= sender.Location.X))
            {
                return false;
            }
            else if ((b.Y >= sender.Location.Y + sender.Size.Height) || (a.Y <= sender.Location.Y) || (a.Y >= b.Y))
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)
        {
            a.X += _x;
            a.Y += _y;
            b.X += _x;
            b.Y += _y;
            c.X += _x;
            c.Y += _y;
            if (locationCheck(sender) == false)
            {
                a.X -= _x;
                a.Y -= _y;
                b.X -= _x;
                b.Y -= _y;
                c.X -= _x;
                c.Y -= _y;
            }
        }
        public override void resize(PictureBox sender, int _d)
        {
            a.Y = a.Y - _d / 2;
            b.X = b.X + _d / 2;
            b.Y = b.Y + _d / 2;
            c.X = c.X - _d / 2;
            c.Y = c.Y + _d / 2;
            if (locationCheck(sender) == false)
            {
                a.Y = a.Y + _d / 2;
                b.X = b.X - _d / 2;
                b.Y = b.Y - _d / 2;
                c.X = c.X + _d / 2;
                c.Y = c.Y - _d / 2;
            }
        }
        override public bool isChecked(MouseEventArgs e)
        {
            int p1 = (a.X - e.X) * (b.Y - a.Y) - (b.X - a.X) * (a.Y - e.Y);
            int p2 = (b.X - e.X) * (c.Y - b.Y) - (c.X - b.X) * (b.Y - e.Y);
            int p3 = (c.X - e.X) * (a.Y - c.Y) - (a.X - c.X) * (c.Y - e.Y);
            if ((p1 >= 0 && p2 >= 0 && p3 >= 0) || (p1 <= 0 && p2 <= 0 && p3 <= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF()
        {
            return fl;
        }
        override public void slctT()
        {
            fl = true;
        }
        override public void slctF()
        {
            fl = false;
        }
        override public String ret()
        {
            return "Треугольник";
        }
        override public Point getP()
        {
            Point p = new Point(a.X, a.Y + 30);
            return p;
        }
    }
    class CSection : Shape
    {
        private Point a, b; //точки
        private bool f;
        public float angle; //коэф наклона
        public CSection(int x1, int y1, int x2, int y2, Color _clr)
        {
            a.X = x1;
            a.Y = y1;
            b.X = x2;
            b.Y = y2;
            f = true;
            clr = _clr;
            if (b.X - a.X != 0)
            {
                angle = (b.Y - a.Y) / (b.X - a.X); //коэфф наклона
            }
            else
            {
                angle = 1000000;
            }

        }
        public override void save(StreamWriter _file) //сохранение объекта
        {
            _file.WriteLine("L"); //пишем, что записываемый объект - отрезок
            _file.WriteLine(a.X); //записываем его данные
            _file.WriteLine(a.Y);
            _file.WriteLine(b.X);
            _file.WriteLine(b.Y);
            _file.WriteLine(angle);
            _file.WriteLine(clr.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            a.X = Convert.ToInt32(_file.ReadLine());
            a.Y = Convert.ToInt32(_file.ReadLine());
            b.X = Convert.ToInt32(_file.ReadLine());
            b.Y = Convert.ToInt32(_file.ReadLine());
            angle = Convert.ToInt32(_file.ReadLine());
            clr = Color.FromName(_file.ReadLine());
        }
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g) //метод для рисования на pictureBox
        {

            Pen pen = new Pen(clr);
            if (f == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawLine(pen, a, b);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender)
        {
            if ((a.X >= sender.Location.X + sender.Size.Width) || (a.X <= sender.Location.X))
            {
                return false;
            }
            else if ((a.Y >= sender.Location.Y + sender.Size.Height) || (a.Y <= sender.Location.Y))
            {
                return false;
            }
            else if ((b.X >= sender.Location.X + sender.Size.Width) || (b.X <= sender.Location.X))
            {
                return false;
            }
            else if ((b.Y >= sender.Location.Y + sender.Size.Height) || (b.Y <= sender.Location.Y))
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int x, int y)
        {
            a.X = a.X + x;
            a.Y = a.Y + y;
            b.X = b.X + x;
            b.Y = b.Y + y;
            if (locationCheck(sender) == false)
            {
                a.X = a.X - x;
                a.Y = a.Y - y;
                b.X = b.X - x;
                b.Y = b.Y - y;
            }
        }
        public override void resize(PictureBox sender, int _d)
        {
            if (angle != 0)
            {
                a.X = (int)((float)a.X + angle * _d);
                a.Y = (int)((float)a.Y - angle * _d);
                b.X = (int)((float)b.X - angle * _d);
                b.Y = (int)((float)b.Y + angle * _d);
                if (locationCheck(sender) == false)
                {
                    a.X = (int)((float)a.X - angle * _d);
                    a.Y = (int)((float)a.Y + angle * _d);
                    b.X = (int)((float)b.X + angle * _d);
                    b.Y = (int)((float)b.Y - angle * _d);
                }
            }
        }

        override public bool isChecked(MouseEventArgs e)
        {

            if (a.X > b.X)
            {
                if (a.Y < b.Y)
                {
                    if (e.X < a.X && e.X > b.X && e.Y > a.Y && e.Y < b.Y)
                    {
                        return true;
                    }
                }
                else if (a.Y > b.Y)
                {
                    if (e.X < a.X && e.X > b.X && e.Y < a.Y && e.Y > b.Y)
                    {
                        return true;
                    }
                }
            }
            else if (a.X < b.X)
            {
                if (a.Y < b.Y)
                {
                    if (e.X > a.X && e.X < b.X && e.Y > a.Y && e.Y < b.Y)
                    {
                        return true;
                    }
                }
                else if (a.Y > b.Y)
                {
                    if (e.X > a.X && e.X < b.X && e.Y < a.Y && e.Y > b.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        override public bool getF()
        {
            return f;
        }
        override public void slctT()
        {
            f = true;
        }
        override public void slctF()
        {
            f = false;
        }
        override public String ret()
        {
            return "Отрезок";
        }
        override public Point getP()
        {
            return a;
        }
    }
    class Storage
    {
        private Shape[] stor;
        private int count, maxcount;
        List<Observer> observers;
        List<CGlueObject> obss;
        public Storage(int _maxcount)
        {
            maxcount = _maxcount;
            count = 0;
            stor = new Shape[_maxcount];
            for (int i = 0; i < _maxcount; i++)
                stor[i] = null;
            observers = new List<Observer>();
            obss = new List<CGlueObject>();
        }
        public void addObj(Shape obj)
        {
            if (count >= maxcount)
            {
                Array.Resize(ref stor, count + 1);
                stor[count] = obj;
                count++;
                maxcount++;
                for (int i = 0; i < count - 1; i++)
                {
                    stor[i].slctF();
                }
            }
            else if (count == 0)
            {
                stor[count] = obj;
                count++;
            }
            else
            {
                stor[count] = obj;
                count++;
                for (int i = 0; i < count - 1; i++)
                {
                    stor[i].slctF();
                }
            }
            notifyObservers();
        }
        public void deleteObj(int ind)
        {
            stor[ind] = null;
            count--;
            for (int i = ind; i < count; i++)
            {
                stor[i] = stor[i + 1];
            }
            stor[count] = null;
        }
        public void drawAll(PictureBox sender, Bitmap bmp, Graphics g)
        {
            g.Clear(Color.WhiteSmoke); //очистка рисунка
            for (int i = 0; i < count; i++)
            {
                if (stor[i] != null)
                {
                    stor[i].draw(sender, bmp, g);
                }
            }
            if (count == 0)
            {
                sender.Image = bmp;
            }
        }
        public int getCount()
        {
            return count;
        }
        public void allObjUnselect()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        stor[i].slctF();
                    }
                }

            }
            notifyObservers();
        }
        public void delWhenClicedDel()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        deleteObj(i);
                    }
                }
            }
            notifyObservers();
        }
        public bool checkSelectNotCtrl(MouseEventArgs e)
        {
            for (int i = 0; i < count; i++)
            {
                if (stor[i] != null)
                {
                    if (stor[i].isChecked(e) == true)
                    {
                        allObjUnselect();
                        stor[i].slctT();
                        notifyObservers();
                        return true;
                    }
                }
            }
            notifyObservers();
            return false;
        }
        public bool checkSelectCtrl(MouseEventArgs e)
        {
            for (int i = 0; i < count; i++)
            {
                if (stor[i] != null)
                {
                    if (stor[i].isChecked(e) == true)
                    {
                        stor[i].slctT();
                        notifyObservers();
                        return true;
                    }
                }
            }
            notifyObservers();
            return false;
        }
        public void move(PictureBox sender, int _x, int _y)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        stor[i].move(sender, _x, _y);
                    }
                }
            }
            notifyObss();
        }
        public void resize(PictureBox sender, int _d)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        stor[i].resize(sender, _d);
                    }
                }
            }
        }
        public void changeColor(Color _clr)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        stor[i].changeColor(_clr);
                    }
                }
            }
        }
        public void createGroup() //метод создания группы
        {
            int sum = 0; //количество выделенных объектов
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i].getF() == true)
                    {
                        sum++; //если объект выделен, то sum увеличиваем
                    }
                }
            }
            if (sum >= 2) //если выделено больше одного объекта
            {
                CGroup group = new CGroup(sum); //создаем группу
                for (int i = count - 1; i >= 0; i--)
                {
                    if (stor[i] != null)
                    {
                        if (stor[i].getF() == true) //если объект выделен
                        {
                            group.addObj(stor[i]); //добавляем его в группу                           
                            deleteObj(i);              //удаляем объект из хранилища
                        }
                    }
                }
                addObj(group);//добавляем заполненную группу в хранилище
                notifyObservers();
            }
        }
        public void deleteGroup() //метод удаления группы (разгруппировка)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (stor[i] != null)
                {
                    if (stor[i] is CGroup && stor[i].getF() == true)
                    {
                        CGroup tgroup = (CGroup)stor[i];
                        for (int j = tgroup.getCount() - 1; j >= 0; j--)
                        {
                            addObj(tgroup.group[j]);
                            tgroup.deleteObj(j);
                        }
                    }
                }
            }
            notifyObservers();
        }
        public void save() //функция сохранения хранилища в файл
        {

            string path = @"D:\OOPLab7DataTXT\data.txt"; //путь до файла
            StreamWriter cfile = new StreamWriter(path, false); //создаем записыватель файла
            cfile.WriteLine(count); //записываем размер хранилища
            for (int i = 0; i < count; i++)
            {
                if (stor[i] != null) //если объект существует
                {
                    {
                        stor[i].save(cfile); //сохраняем его
                    }
                }
            }
            cfile.Close();
        }
        public void load() //выгрузка объектов из файла в хранилище
        {
            string path = @"D:\OOPLab7DataTXT\data.txt"; //путь до файла
            CShapeFactory factory = new CShapeFactory(); //factory для создания объектов
            StreamReader sr = new StreamReader(path); //читатель файла
            char code;  //код, определяюший тип объекта
            count = Convert.ToInt32(sr.ReadLine());
            maxcount = 100;
            stor = new Shape[maxcount]; //создаем хранилище определенного размера
            for (int i = 0; i < count; i++)
            {
                code = Convert.ToChar(sr.ReadLine()); //считываем тип объекта
                stor[i] = factory.createShape(code); //factory создает объект определенного типа
                if (stor[i] != null)
                {
                    stor[i].load(sr); //считываем информацию о объекте из файла
                }
            }
            sr.Close(); //закрываем файл

        }
        public void addObserver(Observer obs)
        {
            observers.Add(obs);
        }
        public void notifyObservers()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].update(this);
            }
        }
        public Shape retObj(int ind)
        {
            return stor[ind];
        }
        public void addObss(CGlueObject obs)
        {
            obss.Add(obs);
        }
        public void notifyObss()
        {
            for (int i = 0; i < obss.Count; i++)
            {
                obss[i].update(this);
            }
        }
    }
    class CGroup : Shape
    {
        private bool f;
        public Shape[] group;
        private int count, maxcount;
        public CGroup(int _maxcount)
        {
            maxcount = _maxcount; count = 0;
            group = new Shape[maxcount];
            f = true;
            for (int i = 0; i < maxcount; i++)
                group[i] = null;
        }
        ~CGroup()
        {
            for (int i = 0; i < count; i++)
            {
                group[i] = null;
            }
            group = null;
        }
        public int getCount()
        {
            return count;
        }
        public bool addObj(Shape obj) //добавление объекта
        {
            if (count >= maxcount)
            {
                return false;
            }
            group[count] = obj;
            count++;
            return true;
        }
        public void deleteObj(int index)
        {
            group[index] = null;
            count--;
            for (int i = index; i < count; i++)
            {
                group[i] = group[i + 1];
            }

            group[count] = null;
        }
        public override void save(StreamWriter _file) //сохранение объекта
        {
            _file.WriteLine("G"); //пишем, что записываемый объект - группа
            _file.WriteLine(count); //записываем размер группы
            for (int i = 0; i < count; i++)
            {
                if (group[i] != null) //если объект существует
                {
                    {
                        group[i].save(_file); //сохраняем его
                    }
                }
            }
        }
        public override void load(StreamReader _file)
        {
            CShapeFactory factory = new CShapeFactory(); //factory для создания объектов
            char code;  //код, определяюший тип объекта
            count = Convert.ToInt32(_file.ReadLine());
            maxcount = count;
            group = new Shape[count]; //создаем хранилище определенного размера
            for (int i = 0; i < count; i++)
            {
                code = Convert.ToChar(_file.ReadLine()); //считываем тип объекта
                group[i] = factory.createShape(code); //factory создает объект определенного типа
                if (group[i] != null)
                {
                    group[i].load(_file); //считываем информацию о объекте из файла
                }
            }
        }
        public override void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
            for (int i = 0; i < count; i++)
            {
                group[i].draw(sender, bmp, g);
            }
        }
        public override void move(PictureBox sender, int _x, int _y)
        {
            for (int i = 0; i < count; i++)
            {
                group[i].move(sender, _x, _y);
            }
        }
        public override void changeColor(Color _clr)
        {
            for (int i = 0; i < count; i++)
            {
                group[i].changeColor(_clr);
            }
        }
        public override void resize(PictureBox sender, int _d)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (group[i] != null)
                {
                    group[i].resize(sender, _d);
                }
            }
        }
        public override bool isChecked(MouseEventArgs e)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (group[i] != null)
                {
                    if (group[i].isChecked(e) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        override public bool getF() //получение значения " выделенный/не выделенный" у объекта
        {
            return f;
        }
        override public void slctT() //изменение значения f на true
        {
            f = true;
            for (int i = 0; i < count; i++)
            {
                group[i].slctT();
            }
        }
        override public void slctF() //изменение значения f на false
        {
            f = false;
            for (int i = 0; i < count; i++)
            {
                group[i].slctF();
            }
        }
        override public String ret()
        {
            return "Группа";
        }
        public Shape retObj(int ind)
        {
            return group[ind];
        }
    }
    class CShapeFactory
    {
        public Shape createShape(char code)
        {
            Shape shape = null;
            switch (code)
            {
                case 'C':
                    shape = new CCircle(0, 0, 0, Color.Black);
                    break;
                case 'R':
                    shape = new CSquare(0, 0, 0, Color.Black);
                    break;
                case 'T':
                    shape = new CTriangle(0, 0, 0, 0, 0, 0, Color.Black);
                    break;
                case 'L':
                    shape = new CSection(0, 0, 0, 0, Color.Black);
                    break;
                case 'G':
                    shape = new CGroup(0);
                    break;
                case 'O':
                    shape = new CGlueObject(0, 0, 0, Color.Black);
                    break;
                default:
                    break;

            }
            return shape;
        }
    }
    class CGlueObject : Shape
    {
        private int x, y, l;
        private bool fl;
        Storage str;
        List<Shape> gluedObjs = new List<Shape>();
        public CGlueObject(int _x, int _y, int _l, Color _clr)
        {
            x = _x;
            y = _y;
            l = _l;
            fl = true;
            clr = _clr;
        }
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g)
        {
            Rectangle rect = new Rectangle(x - l / 2, y - l / 2, l, l);
            Pen pen = new Pen(Color.BlueViolet);
            if (fl == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawRectangle(pen, rect);
            sender.Image = bmp;
        }
        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            if (e.X >= x - l / 2 && e.Y >= y - l / 2 && e.X <= x + l / 2 && e.Y <= y + l / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF()
        {
            return fl;
        }
        override public void slctT()
        {
            fl = true;
        }
        override public void slctF()
        {
            fl = false;
        }
        private bool checkLocation(PictureBox sender)
        {
            if ((x + l / 2 >= sender.Location.X + sender.Size.Width) || x - l / 2 <= sender.Location.X || l < 1)
            {
                return false;
            }
            else if ((y + l / 2 >= sender.Location.Y + sender.Size.Height) || y - l / 2 <= sender.Location.Y || l < 1)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)
        {
            x = x + _x;
            y = y + _y;
            if (checkLocation(sender) == false)
            {
                x = x - _x;
                y = y - _y;
            }
            for (int i = 0; i < gluedObjs.Count; i++)
            {
                gluedObjs[i].move(sender, _x, _y);
            }
        }
        public override void resize(PictureBox sender, int _d)
        {
            l = l + _d / 2;
            if (checkLocation(sender) == false)
            {
                l = l - _d / 2;
            }
        }
        public override void save(StreamWriter _file)
        {
            _file.WriteLine("O");
            _file.WriteLine(x);
            _file.WriteLine(y);
            _file.WriteLine(l);
            _file.WriteLine(clr.ToKnownColor());
        }
        public override void load(StreamReader _file)
        {
            x = Convert.ToInt32(_file.ReadLine());
            y = Convert.ToInt32(_file.ReadLine());
            l = Convert.ToInt32(_file.ReadLine());
            clr = Color.FromName(_file.ReadLine());
        }
        override public String ret()
        {
            return "Липкий";
        }
        public CGlueObject Add()
        {
            return this;
        }
        public void addStor(Storage _str)
        {
            str = _str;
        }
        override public Point getP()
        {
            Point p = new Point(x, y);
            return p;
        }
        public void update(Storage stor)
        {
            for (int i = 0; i < stor.getCount(); i++)
            {
                if (x - stor.retObj(i).getP().X < l && y - stor.retObj(i).getP().Y < l)
                {
                    gluedObjs.Add(stor.retObj(i));
                }
            }
        }
    }

    class Observer
    {
        private Storage stor;
        TreeView tree;
        public Observer(Storage _stor, TreeView sender)
        {
            stor = _stor;
            tree = sender;
        }
        public void update(Storage _stor)
        {
            TreeNode Node = new TreeNode();
            Node.Text = "Хранилище";
            tree.Nodes.Clear();
            tree.Nodes.Add(Node);
            for (int i = 0; i < _stor.getCount(); i++)
            {
                updTree(_stor.retObj(i), tree.Nodes[0]);
            }

        }
        private void updTree(Shape obj, TreeNode tn)
        {
            TreeNode Node = new TreeNode();
            Node.Text = obj.ret();
            if (obj.getF())
            {
                Node.Checked = true;
            }
            tn.Nodes.Add(Node);
            if (obj.ret() == "Группа")
            {
                for (int i = 0; i < ((CGroup)obj).getCount(); i++)
                {
                    updTree(((CGroup)obj).retObj(i), tn.Nodes[tn.Nodes.Count - 1]);
                }
            }
        }

    }

}
