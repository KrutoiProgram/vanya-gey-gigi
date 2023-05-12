using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AudioBeta1._0.Models
{
    public class Track
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public BitmapImage Photo { get; set; }
        public string Path { get; set; }
    }
}
