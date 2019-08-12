using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quotes {

    public const string UI_DEFAULT_QUOTE = "Tell me who to kill. Point the camera to a target then fire.";

    public const string UI_DEFAULT_QUOTE_DIAMONDS = "Tell me who to kill. Point the camera to an enemy and press fire.";
    public const string UI_DEFAULT_QUOTE_CLUBS = "Ready to serve. Point the camera to an enemy and press fire.";
    public const string UI_DEFAULT_QUOTE_SPADES = "Awaiting orders. Point the camera to an enemy and press fire.";
    public const string UI_DEFAULT_QUOTE_HEARTS = "Which ones, sir? Point the camera to an enemy and press fire.";
	public const string UI_DEFAULT_QUOTE_MULTI = "Together, we are stronger.  Point the camera to an enemy and press fire.";

	public const string ANNOUNCEMENT_HIGHER = "Only attacks with the value greater than or equal to the target's defense are effective.";
	public const string ANNOUNCEMENT_LOWER = "Only attacks with the value less than or equal to the target's defense are effective.";

	public const string ANNOUNCEMENT_RED_CARDS_ONLY = "Only cards marked by a HEART or DIAMOND can be used this turn. We wish for strict compliance. All violators will be burned.";
    public const string ANNOUNCEMENT_WHITE_CARDS_ONLY = "Only cards marked as SPADES or CLUBS can be used this turn. We wish for strict compliance. All violators will be burned.";
    public const string ANNOUNCEMENT_EAT_ME = "Let all soldiers eat the unknown cake. All of our soldiers are stronger by 1 health.";
    public const string ANNOUNCEMENT_DRINK_ME = "Let all soldiers drink the unknown liquid. All of our soldiers are weakened by 1 health.";
	public const string ANNOUNCEMENT_DRAW_CARD = "Voluntary conscription is mandatory. Draw a card.";
	public const string ANNOUNCEMENT_REPLENISH_DEFENSE = "We must reinforce our defenses. Replenish our missing defense by a random number.";
	public const string ANNOUNCEMENT_REPLENISH_DECK = "The armory has been resupplied. Replenish the deck.";

	public const string ANNOUNCEMENT_BOMB = "Pyrotechnics are necessary for celebrations. We are granted a bomb charge.";
	public const string ANNOUNCEMENT_SUMMON = "The hero has arrived. We are granted hero skill charges.";
	public const string ANNOUNCEMENT_MOVEP2 = "You are given 2 extra moves this turn.";
	public const string ANNOUNCEMENT_MOVEP3 = "You are given 3 extra moves this turn.";
	public const string ANNOUNCEMENT_MOVEP4 = "You are given 4 extra moves this turn.";
	public const string ANNOUNCEMENT_MOVEP5 = "You are given 5 extra moves this turn.";
	public const string ANNOUNCEMENT_MOVEX2 = "Your moves for this turn are multiplied by 2!";
	public const string ANNOUNCEMENT_MOVED2 = "The number of moves you have are halved!";
	public const string ANNOUNCEMENT_STRDEF = "The shield shipment has arrived! All of our defense soldiers are stronger by 1 health.";
	public const string ANNOUNCEMENT_STRHAND = "The legendary blacksmith has come to provide better weapons! All soldiers in hand are stronger by 1 attack.";


	public const string HERO_DESC_ALICE = "Killed a giant lizard when she was seven. They let her keep the sword as a souvenir.";
	public const string HERO_DESC_HATTER = "Stuck in a perpetual tea party after 'killing the time'.";
	public const string HERO_DESC_CHESHIRE = "A mischevious and mysterious creature. ";

	public const string HERO_SKILL_ALICE = "VORPAL SWORD";
	public const string HERO_SKILL_HATTER = "MAD AS A HATTER";
	public const string HERO_SKILL_CHESHIRE = "VANISH";

	public const string HERO_SKILL_DESC_ALICE = "All cards in your HAND have +1 ATTACK for THIS TURN.";
	public const string HERO_SKILL_DESC_HATTER = "TEST YOUR LUCK: RESPIN the ROULETTE.";
	public const string HERO_SKILL_DESC_CHESHIRE = "Remove your enemy's skill charges.";

    public const string NO_SKILL_HINT = "You have no skill points left! You can get SKILL POINTS from the roulette during the SPIN PHASE.";
    public const string NO_BOMB_HINT = "You have no bombs! You can get BOMBS as item drops from the roulette during the SPIN PHASE.";

    public const string HINT_DEFAULT = "Point camera to ARENA target to view the board.Drag a card to the center of the screen to attack.";

	public const string BOMB_TOOLTIP = "The bomb destroys random soldiers on both of the players' defenses.";

    public static string GetHeroDescription(Hero.Type hero) {
		switch (hero) {
			case Hero.Type.ALICE: return HERO_DESC_ALICE;
			case Hero.Type.HATTER: return HERO_DESC_HATTER;
			case Hero.Type.CHESHIRE: return HERO_DESC_CHESHIRE;
		}
		return "";
	}

	public static string GetHeroSkill(Hero.Type hero) {
		switch (hero) {
			case Hero.Type.ALICE: return HERO_SKILL_ALICE;
			case Hero.Type.HATTER: return HERO_SKILL_HATTER;
			case Hero.Type.CHESHIRE: return HERO_SKILL_CHESHIRE;
		}
		return "";
	}

	public static string GetHeroSkillDesc(Hero.Type hero) {
		switch (hero) {
			case Hero.Type.ALICE: return HERO_SKILL_DESC_ALICE;
			case Hero.Type.HATTER: return HERO_SKILL_DESC_HATTER;
			case Hero.Type.CHESHIRE: return HERO_SKILL_DESC_CHESHIRE;
		}
		return "";
	}

	public static string GetAttackUIQuote(int player, Card.Suit suit) {
        switch(suit) {
            case Card.Suit.CLUBS: return GetClubsQuote(player);
            case Card.Suit.DIAMONDS: return GetDiamondsQuote(player);
            case Card.Suit.SPADES: return GetSpadesQuote(player);
            case Card.Suit.HEARTS: return GetHeartsQuote(player);
        }
        return UI_DEFAULT_QUOTE;
    }


    /// <summary>
    /// Contains all the quotes for clubs.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static string GetClubsQuote(int player) {
        switch (player) {
            case 1:
                break;
            case 2:
                break;
        }
        return UI_DEFAULT_QUOTE_CLUBS;
    }

    /// <summary>
    /// Contains all the quotes for diamonds.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static string GetDiamondsQuote(int player) {
        switch (player) {
            case 1:
                break;
            case 2:
                break;
        }
        return UI_DEFAULT_QUOTE_DIAMONDS;
    }

    /// <summary>
    /// Contains all the quotes for spades.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static string GetSpadesQuote(int player) {
        switch (player) {
            case 1:

                break;
            case 2:
                break;
        }
        return UI_DEFAULT_QUOTE_SPADES;
    }
    /// <summary>
    /// Contains all the quotes for hearts.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static string GetHeartsQuote(int player) {
        switch (player) {
            case 1:

                break;
            case 2:
                break;
        }
        return UI_DEFAULT_QUOTE_HEARTS;
    }

}
