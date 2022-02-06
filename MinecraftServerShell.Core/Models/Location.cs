using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class Location
    {
        public Location(double x, double y, double z, string dimensionId)
        {
            Coordinate = new()
            {
                X = x,
                Y = y,
                Z = z
            };

            Dimension = dimensionId.ParseDimension();
        }

        public Location(Coordinate coordinate, Dimension dimension)
        {
            Coordinate = coordinate;
            Dimension = dimension;
        }

        public Coordinate Coordinate { get; set; }

        public Dimension Dimension { get; set; }
    }

    public class Coordinate
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }

    public enum Dimension
    {
        Overworld,
        TheNether,
        TheEnd,
        Invalid
    }

    public static class DimensionExtension
    {
        public static Dimension ParseDimension(this string dimensionId)
        {
            if (dimensionId == "minecraft:overworld")
            {
                return Dimension.Overworld;
            }
            else if (dimensionId == "minecraft:the_nether")
            {
                return Dimension.TheNether;
            }
            else if (dimensionId == "minecraft:the_end")
            {
                return Dimension.TheEnd;
            }
            else
            {
                return Dimension.Invalid;
            }
        }
    }
}
