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
            float margin = 20;
            float width = dirtyRect.Width - 2 * margin;
            float height = dirtyRect.Height - 2 * margin - 30;  // 텍스트 표시 공간을 추가 확보
            if (Records == null || Records.Count < 2) return;

            float maxScore = Records.Max(r => r.Score);
            float minScore = Records.Min(r => r.Score);
            float range = maxScore - minScore;
            range = range <= 0 ? 1 : range;

            // 데이터 포인트 간의 최소 간격
            float minGap = 50; // 최소 50 픽셀 간격
            float xScale = Math.Max(minGap, width / Records.Count);

            PathF path = new PathF();
            for (int i = 0; i < Records.Count; i++)
            {
                float x = margin + i * xScale; // 동적 간격 조정
                float y = margin + ((maxScore - Records[i].Score) / range) * height;
                if (i == 0)
                    path.MoveTo(x, y);
                else
                    path.LineTo(x, y);

                // RequestTime을 그래프 아래에 표시
                string timeLabel = DateTime.Parse(Records[i].RequestTime).ToString("MM/dd");
                canvas.FontSize = 12;
                canvas.FontColor = Colors.DarkGray;

                // 추정 너비 사용: 문자열 길이 * 글자당 평균 너비(약 6픽셀) 
                float textWidth = timeLabel.Length * 6;

                // 문자열 중앙 정렬을 위한 수정
                canvas.DrawString(timeLabel, x, height + margin * 1.5f + 10, HorizontalAlignment.Center);
                //canvas.DrawString(timeLabel, x - textWidth / 2, height + margin * 1.5f + 10, textWidth, 20, HorizontalAlignment.Left, VerticalAlignment.Top);
            }

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            canvas.DrawPath(path);
        }

    }
}
