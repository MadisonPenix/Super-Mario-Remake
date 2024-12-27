using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TeamMilkGame.Collision;
using TeamMilkGame.Commands;

namespace TeamMilkGame.XML
{
    public class XMLCollisionHandler
    {
        string xmlCollisionPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "./GameXMLFiles/CollisionResponseTable.xml");

        private static Dictionary<string, ICollidable.CollisionType> collisionDictionary;
        private static Dictionary<string, Type> commandDictionary;
        private static Dictionary<string, ICollideSide.Side> sideDictionary;
        private IDictionary<HashSet<ICollidable.CollisionType>, IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>> CollisionTable;

        public XMLCollisionHandler(IDictionary<HashSet<ICollidable.CollisionType>, IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>> CollisionTable)
        {
            this.CollisionTable = CollisionTable;

            collisionDictionary = new Dictionary<string, ICollidable.CollisionType>();
            collisionDictionary.Add("Mario", ICollidable.CollisionType.Mario);
            collisionDictionary.Add("SuperMario", ICollidable.CollisionType.SuperMario);
            collisionDictionary.Add("Enemy", ICollidable.CollisionType.Enemy);
            collisionDictionary.Add("Block", ICollidable.CollisionType.Block);
            collisionDictionary.Add("Projectile", ICollidable.CollisionType.Projectile);
            collisionDictionary.Add("FireFlower", ICollidable.CollisionType.FireFlower);
            collisionDictionary.Add("Flagpole", ICollidable.CollisionType.Flagpole);
            collisionDictionary.Add("Coin", ICollidable.CollisionType.Coin);
            collisionDictionary.Add("ExtraLife", ICollidable.CollisionType.ExtraLife);
            collisionDictionary.Add("Star", ICollidable.CollisionType.Star);
            collisionDictionary.Add("Mushroom", ICollidable.CollisionType.Mushroom);
            collisionDictionary.Add("StarMario", ICollidable.CollisionType.StarMario);
            collisionDictionary.Add("Pipe", ICollidable.CollisionType.Pipe);

            sideDictionary = new Dictionary<string, ICollideSide.Side>();
            sideDictionary.Add("Left", ICollideSide.Side.Left);
            sideDictionary.Add("Right", ICollideSide.Side.Right);
            sideDictionary.Add("Top", ICollideSide.Side.Top);
            sideDictionary.Add("Down", ICollideSide.Side.Down);
            sideDictionary.Add("Null", ICollideSide.Side.Null);

            commandDictionary = new Dictionary<string, Type>();
            commandDictionary.Add("BouncePlayer", typeof(BouncePlayer));
            commandDictionary.Add("BounceProjectile", typeof(BounceProjectile));
            //commandDictionary.Add("BounceStarDown", typeof(BounceStarDown));
            commandDictionary.Add("ChangeEnemyDirection", typeof(ChangeEnemyDirection));
            commandDictionary.Add("ChangeItemDirection", typeof(ChangeItemDirection));
            commandDictionary.Add("DescendFlagpole", typeof(DescendFlagpole));
            commandDictionary.Add("DamageEnemy", typeof(DamageEnemy));
            commandDictionary.Add("DamagePlayer", typeof(DamagePlayer));
            commandDictionary.Add("DeleteItem", typeof(DeleteItem));
            commandDictionary.Add("DeleteProjectile", typeof(DeleteProjectile));
            commandDictionary.Add("FireUp", typeof(FireUp));
            commandDictionary.Add("HitBlock", typeof(HitBlock));
            commandDictionary.Add("MoveEnemyUp", typeof(MoveEnemyUp));
            commandDictionary.Add("MoveItemUp", typeof(MoveItemUp));
            commandDictionary.Add("MushroomUp", typeof(MushroomUp));
            commandDictionary.Add("StarUp", typeof(StarUp));
            commandDictionary.Add("MovePlayerLeft", typeof(MovePlayerLeft));
            commandDictionary.Add("MovePlayerRight", typeof(MovePlayerRight));
            commandDictionary.Add("MovePlayerUp", typeof(MovePlayerUp));
            commandDictionary.Add("MovePlayerDown", typeof(MovePlayerDown));
        }

        public void readXML()
        {
            XmlReader r = XmlReader.Create(xmlCollisionPath);
            while (r.NodeType != XmlNodeType.Element)
                r.Read();
            XElement e = XElement.Load(r);
            parseXMLCollisionResponse(e);
        }

        private IDictionary<HashSet<ICollidable.CollisionType>, IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>> parseXMLCollisionResponse(XElement e)
        {
            foreach (XElement collisionType in e.Elements())
            {
                HashSet<ICollidable.CollisionType> Objs = parseCollisionObjects(collisionType);
                IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>> SideTypes = parseSideTypes(collisionType);
                CollisionTable.Add(Objs, SideTypes);
            }
            return CollisionTable;
        }

        private HashSet<ICollidable.CollisionType> parseCollisionObjects(XElement collisionType)
        {
            HashSet<ICollidable.CollisionType> Objs = new HashSet<ICollidable.CollisionType>();
            Objs.Add(collisionDictionary[collisionType.Attribute("collider").Value]);
            Objs.Add(collisionDictionary[collisionType.Attribute("collidee").Value]);
            return Objs;
        }

        private IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>> parseSideTypes(XElement collisionType)
        {
            IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>> SideTypes = new Dictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>();
            foreach (XElement side in collisionType.Elements())
            {
                IDictionary<ICollidable.CollisionType, Type> collisionResponse = parseCommands(side);
                SideTypes.Add(sideDictionary[side.Name.LocalName], collisionResponse);
            }
            return SideTypes;
        }

        private IDictionary<ICollidable.CollisionType, Type> parseCommands(XElement side)
        {
            IDictionary<ICollidable.CollisionType, Type> collisionResponse = new Dictionary<ICollidable.CollisionType, Type>();
            foreach (XElement command in side.Elements())
            {
                // lemme keep it real with you I have little to no idea what's
                // going on here but the debugger told me to do it and now the game 
                // doesn't crash when it starts up so I left it
                string Value = command.Attribute("command")?.Value;
                if (Value == null) continue;
                string Key = command.Attribute("type")?.Value;
                if (Key != null)
                    collisionResponse.Add(collisionDictionary[Key],
                        commandDictionary[Value]);
            }

            return collisionResponse;
        }
    }
}
