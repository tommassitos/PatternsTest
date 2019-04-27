using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsTest
{
    //Паттерн Приспособленец(Flyweight) - структурный шаблон проектирования, который позволяет использовать разделяемые объекты сразу в нескольких контекстах.
    //Данный паттерн используется преимущественно для оптимизации работы с памятью.
    //В качестве стандартного применения данного паттерна можно привести следующий пример.Текст состоит из отдельных символов. 
    //Каждый символ может встречаться на одной странице текста много раз. Однако в компьютерной программе было бы слишком накладно выделять память для каждого отдельного символа в тексте. 
    //Гораздо проще было бы определить полный набор символов, например, в виде таблицы из 128 знаков (алфавитно-цифровые символы в разных регистрах, знаки препинания и т.д.). 
    //А в тексте применить этот набор общих разделяемых символов, вместо сотен и тысяч объектов, которые могли бы использоваться в тексте.
    //И как следствие подобного подхода будет уменьшение количества используемых объектов и уменьшение используемой памяти.

    //Паттерн Приспособленец следует применять при соблюдении всех следующих условий:

    //Когда приложение использует большое количество однообразных объектов, из-за чего происходит выделение большого количества памяти

    //Когда часть состояния объекта, которое является изменяемым, можно вынести во вне. 
    //Вынесение внешнего состояния позволяет заменить множество объектов небольшой группой общих разделяемых объектов.

    //Ключевым моментом здесь является разделение состояния на внутренне и внешнее. Внутреннее состояние не зависит от контекста.
    //В примере с символами внутреннее состояние описывается кодом символа из таблицы кодировки. Так как внутреннее состояние не зависит от контекста, то оно может быть разделяемым и поэтому выносится в разделяемые объекты.

    //Внешнее состояние зависит от контекста, является изменчивым. В применении к символам внешнее состояние может представлять положение символа на странице. 
    //То есть код символа может быть использован многими символами, тогда как положение на странице будет для каждого символа индивидуально.

    //При создании приспособленца внешнее состояние выносится. В приспособленце остается только внутреннее состояние. 
    //То есть в примере с символами приспособленец будет хранить код символа.
    class FlyweightTest
    {
        static public void Go()
        {
            double longitude = 37.61;
            double latitude = 55.74;

            HouseFactory houseFactory = new HouseFactory();
            for (int i = 0; i < 5; i++)
            {
                House panelHouse = houseFactory.GetHouse("Panel");
                if (panelHouse != null)
                    panelHouse.Build(longitude, latitude);
                longitude += 0.1;
                latitude += 0.1;
            }

            for (int i = 0; i < 5; i++)
            {
                House brickHouse = houseFactory.GetHouse("Brick");
                if (brickHouse != null)
                    brickHouse.Build(longitude, latitude);
                longitude += 0.1;
                latitude += 0.1;
            }
        }

        abstract class House
        {
            protected int stages; // количество этажей

            public abstract void Build(double longitude, double latitude);
        }

        class PanelHouse : House
        {
            public PanelHouse()
            {
                stages = 16;
            }

            public override void Build(double longitude, double latitude)
            {
                Console.WriteLine("Построен панельный дом из 16 этажей; координаты: {0} широты и {1} долготы",
                    latitude, longitude);
            }
        }
        class BrickHouse : House
        {
            public BrickHouse()
            {
                stages = 5;                
            }

            public override void Build(double longitude, double latitude)
            {
                Console.WriteLine("Построен кирпичный дом из 5 этажей; координаты: {0} широты и {1} долготы",
                    latitude, longitude);
            }
        }

        class HouseFactory
        {
            Dictionary<string, House> houses = new Dictionary<string, House>();
            public HouseFactory()
            {
                houses.Add("Panel", new PanelHouse());
                houses.Add("Brick", new BrickHouse());
            }

            public House GetHouse(string key)
            {
                if (houses.ContainsKey(key))
                    return houses[key];
                else
                    return null;
            }
        }
    }
}
