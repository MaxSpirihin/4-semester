using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeCCtor.Main
{
    class Utils
    {

        public static bool PoiinsIsInRectangle(double x, double y, double centerX, double centerY, double width, double height, double alpha)
	{
		float pointX = (float) (x * Math.Cos(alpha*Math.PI/180) + y
				* Math.Sin(alpha*Math.PI/180));
		float pointY = (float) (-x * Math.Sin(alpha*Math.PI/180) + y
				* Math.Cos(alpha*Math.PI/180));

		float newCenterX = (float) (centerX
				* Math.Cos(alpha*Math.PI/180) + centerY
				* Math.Sin(alpha*Math.PI/180));
		float newCenterY = (float) (-centerX
				* Math.Sin(alpha*Math.PI/180) + centerY
				* Math.Cos(alpha*Math.PI/180));

		if  ((pointX > newCenterX - width/2)
				&& (pointX < newCenterX + width/2)
				&& (pointY > newCenterY - height/2)
				&& (pointY < newCenterY + height/2))
		{
			return true;
		}
		return false;
	}
    }
}
