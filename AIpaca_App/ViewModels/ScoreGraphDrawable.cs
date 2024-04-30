using AIpaca_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.ViewModels
{
    public class ScoreGraphDrawable : IDrawable
    {
        public List<EvRecord>? Records { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);

            if (Records == null || Records.Count == 0)
                return;

            float margin = 20;
            float graphWidth = dirtyRect.Width - 2 * margin;
            float graphHeight = dirtyRect.Height - 2 * margin;
            float xScale = graphWidth / Records.Count;

            for (int i = 0; i < Records.Count - 1; i++)
            {
                var x1 = margin + i * xScale;
                var y1 = margin + (1 - (Records[i].Score / 100f)) * graphHeight;
                var x2 = margin + (i + 1) * xScale;
                var y2 = margin + (1 - (Records[i + 1].Score / 100f)) * graphHeight;

                canvas.StrokeColor = Colors.Black;
                canvas.DrawLine(x1, y1, x2, y2);
            }
        }
    }
}
