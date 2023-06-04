using ModelsLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProject.Tools;
using WpfProject.Windows;

namespace WpfProject.ViewModels
{
    public class ListAirplanesVM : BaseTools
    {
        public Airplane SelectedItem { get; set; }
        private List<AirplanesClass> airplanesClass;
        private List<Airplane> airplane;      
        public Airplane airplanes { get; set; }
        private AirplanesClass airplanesClasses;
        public string Title { get; set; }
        public int Seats { get; set; }
       


        public AirplanesClass SelectedClass
        {
            get => airplanesClasses;
            set
            {
                airplanesClasses = value;
                Signal();
            }
        }

        public List<Airplane> Airplane
        {
            get => airplane;
            set
            {
                airplane = value;

                Signal();
            }
        }     

        public List<AirplanesClass> AirplanesClass
        {
            get => airplanesClass;
            set
            {
                airplanesClass = value;

                Signal();
            }
        }

        public CommandVM DeleteAirplane { get; set; }
        public CommandVM EditAirplane { get; set; }
        public CommandVM Save { get; set; }


        public ListAirplanesVM( )
        {
            Task.Run(async () =>
            {
                var json = await HttpApi.Post("Airplanes", "ListAirplanes", null);
                Airplane = HttpApi.Deserialize<List<Airplane>>(json);

                var json2 = await HttpApi.Post("AirplanesClasses", "ListAirplanesClasses", null);
                AirplanesClass = HttpApi.Deserialize<List<AirplanesClass>>(json2);
            });


            EditAirplane = new CommandVM(async () =>
            {
                airplanes = SelectedItem;
                new AirplaneEdit(airplanes).Show();
            });

            Save = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Airplanes", "save", new Airplane
                {
                    AirplaneTitle = Title,
                    Places = Seats,
                    ClassId = SelectedClass.AirplaneClassId
                });
                Airplane result = HttpApi.Deserialize<Airplane>(json);

                MessageBox.Show("Сохранилось!");
            });

            DeleteAirplane = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Airplanes", "delete", SelectedItem.AirplanesId);

                Task.Run(async () =>
                {
                    var json = await HttpApi.Post("Airplanes", "ListAirplanes", null);
                    Airplane = HttpApi.Deserialize<List<Airplane>>(json);

                    var json2 = await HttpApi.Post("AirplanesClasses", "ListAirplanesClasses", null);
                    AirplanesClass = HttpApi.Deserialize<List<AirplanesClass>>(json);
                });
            });                    
        }
    }
}
