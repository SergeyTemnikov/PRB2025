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

        public int GetXPosition(List<GraphElement> graphElements, Dictionary<int, int> levelWidths, int width)
        {
            var parentElement = graphElements.FirstOrDefault(e => e.Children.Contains(this));
            if (parentElement != null)
            {
                var siblings = parentElement.Children;

                int index = siblings.IndexOf(this);

                int parentWidth = parentElement.Width;

                int levelWidth = levelWidths[Level];

                int totalSiblingsWidth = siblings.Sum(child => child.Width) + (siblings.Count - 1) * ElementSpace;

                int offset = (parentWidth - levelWidth) / 2 + Width + index * ElementSpace + index * WidthElement;

                return parentElement.GetXPosition(graphElements, levelWidths, width) + offset;
            }
            else
            {
                return width / 2 - Width / 2;
            }
        }

                //        // Возвращаем позицию родительского элемента плюс смещение
                //return parentElement.GetXPosition(graphElements, levelWidths, width) - parentWidth / 2 + offset;

                ////var offset = (totalSiblingsWidth - Width) / 2 + index * (Width + ElementSpace);

        public int GetYPosition()
        {
            return ElementSpace + (Level - 1) * (HeightElement + ElementSpace);
        }
    }
}
