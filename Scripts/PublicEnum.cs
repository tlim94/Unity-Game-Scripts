public enum playerID {P1, P2, P3};
public enum blockState {FULL, BROKEN, DESTROYED};
public enum blockType {DIRT, STONE, SAND};
public enum blockLayer {TOP, MID, BOT};

public enum rarityLevel {COMMON, UNCOMMON, RARE};

public struct Item
{
	public string name;
	public string type;
	public int value;
	public rarityLevel rarity;

	public Item(bool fill){
		name = "default";
		type = "weapon";
		value = 0;
		rarity = rarityLevel.COMMON;
	}

	public Item(int num){
		name = num.ToString();
		type = "weapon";
		value = 0;
		rarity = rarityLevel.COMMON;
	}

	public Item(string name, string type, int value, rarityLevel rarity){
		this.name = name;
		this.type = type;
		this.value = value;
		this.rarity = rarity;
	}
}