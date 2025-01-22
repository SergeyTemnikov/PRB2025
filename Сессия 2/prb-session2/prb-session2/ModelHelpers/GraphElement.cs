using prb_session2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace prb_session2.ModelHelpers
{
    public class GraphElement
    {
        public static int WidthElement { get; set; }
        public static int HeightElement { get; set; }
        public static int ElementSpace { get; set; }
        public static int LevelsCount { get; set; } 
        public static int Height { get
            {
                return LevelsCount * HeightElement + LevelsCount * ElementSpace;
            }
        }

        public Departament Parent { get; set; }

        public List<GraphElement> Children { get; set; }
        public int Level { get; set; }
        public int Width { get { 
                
                int count = Children.Count;
                int width = 0;
                if (count > 0)
                {
                    width = Children.Sum(c => c.Width) + (count - 1) * ElementSpace;
                }
                else
                {
                    width = WidthElement;
                }
                return width;
            } }

        public int Offset {  get; set; } 

        public int GetXPosition(List<GraphElement> graphElements, Dictionary<int, int> levelWidths, int width)
        {
             var parentElement = graphElements.FirstOrDefault(e =>  e.Children.Contains(this));
            if (parentElement != null)
            {
                var childrens = parentElement.Children;

                int index = childrens.IndexOf(this);

                int levelWidth = levelWidths[Level];

                int offset = 0;

                for(int i = 0; i < index; i++)
                {
                    offset += childrens[i].Width;
                }

                Offset = offset;

                int parentOffset = parentElement.Offset == 0 ? parentElement.Offset : parentElement.Offset + ElementSpace;

                int finalOffset = 0;

                if(offset == 0)
                {
                    finalOffset = parentOffset + childrens[index].Width / 2 - WidthElement / 2;
                }
                else
                {
                    finalOffset = offset + parentOffset +((levelWidth - offset) / 2 - WidthElement / 2);
                }

                return finalOffset;
            }
            else
            {
                return width / 2 - WidthElement / 2;
            }
        }

        public int GetYPosition()
        {
            return ElementSpace + (Level - 1) * (HeightElement + ElementSpace);
        }
    }
}
