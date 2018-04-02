namespace BlackMoon
{
    /*
	 * Plurable enums are meant to have multiple selections. Singular enums should be exclusive
	 */

    public class TileMods{
		public enum Attributes{
			None, LightBlocker, Actionable, Animated, Sloped, Buildable, Fishable, Plantable, Diggable, Collideable
		}

		//For sound effects
		public enum Type{
			Grass, Dirt, Sand, DeepWater, ShallowWater, Wood, OldWood, Stone, Paved
		}

	}

	public class SubBlockMods{
		public enum Attributes{
			None
		}

		public enum BaseZones {
			Grass, Forest, Arctic, Desert, 
		}
	}

	public class BlockMods{
		
	}

	public class WeatherMods{
		public enum WeatherType{
			Sun, Rain, Snow
		}

		public enum WeatherAdditions{
			Wind, Dust, Ash, Mist, Smoke
		}

		public enum WeatherSeverity{
			Normal, Light, Harsh, Severe
		}

		public enum WeatherTemperature{
			SuperCold, VeryCold, Cold, Normal, Hot, VeryHot, SuperHot
		}
	}

	public class SpriteMods{
		public enum Direction{
			N, E, W, S, NW, NE, SE, SW
		}
	}

	public class InputMods{
		public enum ButtonAction{
			Down, Clicked, Released, Dragging
		}
	}

    public class WindowMods
    {

    }
}

