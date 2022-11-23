using ReactiveUI;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageProcessorGUI.ViewModels
{
    public class LinearStretchingViewModel: ViewModelBase
    {

        private float value;
        public string Text { get; set; } = "0.00";

        public ICommand RefreshCommand => ReactiveCommand.Create(() =>
        {
        Console.WriteLine("Value: " + value);
            Console.WriteLine("Text: " + Text);
        });

        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                Text = value.ToString();
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(Text));
            }
        }
    }
}
