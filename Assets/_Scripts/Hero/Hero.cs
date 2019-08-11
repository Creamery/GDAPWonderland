using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for all heroes.
/// </summary>
public class Hero {

    /// <summary>
    /// Enumerates the different types of heroes.
    /// Default is used to indicate an unset hero.
    /// TODO: Caterpillar
    /// </summary>
    public enum Type {
        DEFAULT,
        ALICE,
        WHITE_ALICE,
        HATTER,
        CHESHIRE,
		QUEEN_OF_HEARTS
    }

   private Type hero = Type.ALICE;

    //private int skillCount;
    private string skillText;

	public Hero(Type hero) {
		this.hero = hero;
		//skillCount = GetMaxSkillCount();
		skillText = Quotes.GetHeroSkillDesc(hero);
		//skillText = GetSkillText();
	}

    /// <summary>
    /// Max skill count getter.
    /// </summary>
    /// <returns></returns>
    public virtual int GetMaxSkillCount() {
        return GameConstants.GetMaxSkillCount(this.GetHero());
    }

    /// <summary>
    /// Gets the current hero.
    /// </summary>
    /// <returns></returns>
    public Type GetHero() {
        return this.hero;
    }

    /// <summary>
    /// Sets a new hero.
    /// </summary>
    /// <param name="newHero"></param>
    public void SetHero(Type newHero) {
        this.hero = newHero;

    }

    /// <summary>
    /// Returns the description of the hero power.
    /// </summary>
    /// <returns></returns>
    public virtual string GetSkillText() {
        return this.skillText;
    }
}
