using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constant variables used throughout the game.
/// </summary>
public class GameConstants {
    public class SkillCount {
        public const int DEFAULT = 3;

        public const int ALICE = 2;
        public const int WHITE_ALICE = 1;
        public const int HATTER = 3;
        public const int CHESHIRE = 3;
		public const int QUEEN_OF_HEARTS = 1;
	}

	public class SkillValues {
		public const int VORPAL_SWORD_BONUS = 1;

	}

    public static int GetMaxSkillCount(Hero.Type type) {
        switch (type) {
            case Hero.Type.ALICE:
                return SkillCount.ALICE;
            case Hero.Type.WHITE_ALICE:
                return SkillCount.WHITE_ALICE;
            case Hero.Type.HATTER:
                return SkillCount.HATTER;
            case Hero.Type.CHESHIRE:
                return SkillCount.CHESHIRE;
			case Hero.Type.QUEEN_OF_HEARTS:
				return SkillCount.QUEEN_OF_HEARTS;
            default:
                return SkillCount.DEFAULT;
        }
    }

	public const bool REPLENISH_AS_ACTION = true;
	public const bool ONE_SHOT_DIRECT = true;
    public const bool HAS_CLEAN_KILL_BONUS = false; // If true, MovesLeft will increment whenever a soldier is killed using an exact value.

    public const int EAT_ME_MODIFIER = 1;
	public const int DRINK_ME_MODIFIER = -1;
	public const int EAT_DRINK_DURATION = 1;

    public const int MAX_PLAYER_LIFE = 1;
    public const int DEFAULT_MOVE_COUNT = 3;
	public const int DEFAULT_MOVE_INCREMENT = 1;
	public const int DEFAULT_MAX_MOVES = 10;
    
    public const int MIN_DRAWN_CARDS = 1;
    public const int MAX_DRAWN_CARDS = 1;
    public const int MAX_HAND_CARDS = 10;
	public const int INITIAL_HAND_CARDS = 5;

    public const int MAX_RANK_1 = 3;
	public const int MAX_RANK_2 = 3;
	public const int MAX_RANK_3 = 3;

	public const int MAX_H_RANK_1 = 3;
	public const int MAX_H_RANK_2 = 2;

    public const int MAX_DEFENSE_SOLDIER = 3;

    public const int MAX_SKILL_ALICE = 2;
    public const int MAX_SKILL_WHITE_ALICE = 1;
    public const int MAX_SKILL_HATTER = 3;
    public const int MAX_SKILL_CHESHIRE = 3;

	public const int MIN_BOMB_ROLL = 1;
	public const int MAX_BOMB_ROLL = 6;

	public const int MIN_REPLENISH_ROLL = 1;
	public const int MAX_REPLENISH_ROLL = 6;

	public const int MIN_REMOVE_CARDS = 1;
	public const int MAX_REMOVE_CARDS = 3;

	public const int MAX_ATTACK_COMBINATION = 2;
}
