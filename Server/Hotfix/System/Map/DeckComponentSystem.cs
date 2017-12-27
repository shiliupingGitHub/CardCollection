﻿using System;
using System.Collections.Generic;
using Model;

namespace Hotfix
{
    [ObjectEvent]
    public class DeckComponentEvent : ObjectEvent<DeckComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public static class DeckComponentSystem
    {
        public static void Awake(this DeckComponent self)
        {
            self.CreateDeck();
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        public static void Shuffle(this DeckComponent self)
        {
            if (self.CardsCount == 54)
            {
                Random random = new Random();
                List<Card> newCards = new List<Card>();
                foreach (var card in self.library)
                {
                    newCards.Insert(random.Next(newCards.Count + 1), card);
                }

                self.library.Clear();
                self.library.AddRange(newCards);
            }
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <returns></returns>
        public static Card Deal(this DeckComponent self)
        {
            Card card = self.library[self.CardsCount - 1];
            self.library.Remove(card);
            return card;
        }

        /// <summary>
        /// 向牌库中添加牌
        /// </summary>
        /// <param name="card"></param>
        public static void AddCard(this DeckComponent self, Card card)
        {
            self.library.Add(card);
        }

        /// <summary>
        /// 创建一副牌
        /// </summary>
        private static void CreateDeck(this DeckComponent self)
        {
            //创建普通扑克
            for (int color = 0; color < 4; color++)
            {
                for (int value = 0; value < 13; value++)
                {
                    Weight w = (Weight)value;
                    Suits s = (Suits)color;
                    Card card = new Card(w, s);
                    self.library.Add(card);
                }
            }

            //创建大小王扑克
            self.library.Add(new Card(Weight.SJoker, Suits.None));
            self.library.Add(new Card(Weight.LJoker, Suits.None));
        }
    }
}
