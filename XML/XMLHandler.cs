using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace TeamMilkGame
{
    public class XMLHandler
    {

        private List<IGameObject> collidables = new List<IGameObject>();
        private List<IGameObject> nonCollidables = new List<IGameObject>();

        public Dictionary<string, List<IGameObject>> objects;

        public bool cameraDisabled;
        public Color backgroundColor;
        public string levelFile;
        public Background levelBackground;
        public double timer;
        public string worldNumber;

        public XMLHandler(Dictionary<string, List<IGameObject>> objects)
        {
            this.objects = objects;
            cameraDisabled = false;
            backgroundColor = Color.CornflowerBlue;
            objects.Add("Collidable", collidables);
            objects.Add("Not_Collidable", nonCollidables);
        }

        public void parseXMLTree(XElement tree)
        {
            foreach (XElement e in tree.Elements())
            {
                switch (e.Name.LocalName)
                {
                    case "Camera":
                        {
                            if (string.Equals(e.Value, "enabled", StringComparison.OrdinalIgnoreCase))
                            {
                                cameraDisabled = false;
                            }
                            else
                            {
                                cameraDisabled = true;
                            }
                            break;
                        }
                    case "BackgroundColor":
                        {

                            //parse background color
                            backgroundColor = ParseColor(e); break;
                        }
                    /*case "Players":
                        {

                            //parse postions for all players
                            ParsePlayerPostions(e); break;
                        }*/
                    default:
                        {
                            foreach (XElement t in e.Elements())
                            {
                                List<IGameObject> collidable = parseXMLGameObject(e, t);

                                foreach (IGameObject obj in collidable)
                                {
                                    if (e.Attribute("collidable").Value == "1")
                                    {
                                        collidables.Add(obj);
                                    }
                                    else
                                    {
                                        nonCollidables.Add(obj);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
        }

        private Color ParseColor(XElement e)
        {
            int R = 0;
            int G = 0;
            int B = 0;

            int alpha = 1;
            foreach (XElement t in e.Elements())
            {
                switch (t.Name.LocalName.ToLower())
                {
                    case "r":
                        {
                            R = Int32.Parse(t.Value);
                            break;
                        }
                    case "g":
                        {
                            G = Int32.Parse(t.Value);
                            break;
                        }
                    case "b":
                        {
                            B = Int32.Parse(t.Value);
                            break;
                        }
                    case "a":
                        {
                            alpha = Int32.Parse(t.Value);
                            break;
                        }
                }
            }
            return new Color(R, G, B, alpha);
        }


        //unused as pipe output locations was implemented instead
        private void ParsePlayerPostions(XElement e)
        {
            IEnumerable<XElement> players = e.Elements();
            foreach (XElement player in players)
            {
                switch (player.Name.LocalName.ToLower())
                {
                    case "player1":
                        {
                            ApplyPlayerAtts(player.Elements(), (IMario)GameObjectManager.Instance.Players[0]);
                            break;
                        }
                    case "player2":
                        {
                            ApplyPlayerAtts(player.Elements(), (IMario)GameObjectManager.Instance.Players[1]);
                            break;
                        }
                    case "player3":
                        {
                            ApplyPlayerAtts(player.Elements(), (IMario)GameObjectManager.Instance.Players[2]);
                            break;
                        }
                    case "player4":
                        {
                            ApplyPlayerAtts(player.Elements(), (IMario)GameObjectManager.Instance.Players[3]);
                            break;
                        }
                }
            }
        }

        private void ApplyPlayerAtts(IEnumerable<XElement> attributes, IMario player)
        {
            //Debug.WriteLine("here");
            foreach (XElement attribute in attributes)
            {
                switch (attribute.Name.LocalName.ToLower())
                {
                    case "position":
                        {
                            string[] posXY = attribute.Value.ToString().Split(' ');
                            List<object> pos = new List<object>();
                            pos.Add(int.Parse(posXY[0]));
                            pos.Add(int.Parse(posXY[1]));
                            coordsToPixels(pos);

                            player.Position = new Vector2(Convert.ToSingle(pos[0]), Convert.ToSingle(pos[1]));
                            break;
                        }
                }
            }
        }

        public List<IGameObject> parseXMLGameObject(XElement objectTypeNode, XElement objectNode)
        {
            Type type = Type.GetType("TeamMilkGame." + objectNode.Name.LocalName);
            Type[] parameterTypes = getParameterTypesArray(objectNode.Elements());

            /*Debug.WriteLine(objectNode.Name.LocalName);
            foreach (Type parameterType in parameterTypes)
            {
                Debug.WriteLine(parameterType);
            }*/
            ConstructorInfo objectConstructor = type.GetConstructor(parameterTypes);


            IEnumerable<XElement> objectAttributeNodes = objectNode.Elements();
            XElement positionNode = objectAttributeNodes.ElementAt(0);
            string[] posXY = positionNode.FirstNode.ToString().Split(' ');

            List<object> parameters = new List<object>();
            parameters.Add(int.Parse(posXY[0]));
            parameters.Add(int.Parse(posXY[1]));
            coordsToPixels(parameters);
            if (parameterTypes.Length > 2)
            {
                for (int i = 2; i < parameterTypes.Length; i++)
                {
                    if (parameterTypes[i] == typeof(IGameItems))
                    {
                        //item in block
                        IEnumerable<XElement> itemsList = objectAttributeNodes.ElementAt(i - 1).Elements();
                        IGameItems item = (IGameItems)parseXMLGameObject(objectAttributeNodes.ElementAt(i - 1), itemsList.ElementAt(0)).ElementAt(0);
                        parameters.Add(item);
                    }
                    else if (parameterTypes[i] == typeof(string))
                    {
                        //portal in pipe
                        string portalXML = objectAttributeNodes.ElementAt(i - 1).Value;
                        parameters.Add(portalXML);
                    }
                    else if (parameterTypes[i] == typeof(Single))
                    {
                        //portal in pipe
                        Single rotation = Convert.ToSingle(objectAttributeNodes.ElementAt(i - 1).Value);
                        parameters.Add(rotation);
                    }
                    else if (parameterTypes[i] == typeof(Vector2))
                    {
                        //portal in pipe
                        parameters = parseVector2Params(objectAttributeNodes.ElementAt(i - 1), parameters);
                    }
                }
            }

            List<IGameObject> objects = new List<IGameObject>();
            objects.Add((IGameObject)objectConstructor.Invoke(parameters.ToArray()));


            //checks attributes for block (length / height)
            if (objectNode.HasAttributes)
            {
                if (objectNode.Attribute("length") != null)
                {
                    int length = int.Parse(objectNode.Attribute("length").Value);
                    for (int i = 0; i < length - 1; i++)
                    {
                        //Should replace these magic numbers
                        int newX = (int)parameters[0] + (16 * 4);
                        parameters[0] = newX;
                        objects.Add((IGameObject)objectConstructor.Invoke(parameters.ToArray()));
                    }
                }
                if (objectNode.Attribute("height") != null)
                {
                    int length = int.Parse(objectNode.Attribute("height").Value);
                    for (int i = 0; i < length - 1; i++)
                    {
                        //Should replace these magic numbers
                        int newY = (int)parameters[1] - (16 * 4);
                        parameters[1] = newY;
                        objects.Add((IGameObject)objectConstructor.Invoke(parameters.ToArray()));
                    }
                }
            }

            return objects;
        }

        private List<object> parseVector2Params(XElement e, List<object> parameters)
        {
            string[] posXY = e.Value.ToString().Split(' ');
            List<object> pos = new List<object>();
            pos.Add(int.Parse(posXY[0]));
            pos.Add(int.Parse(posXY[1]));
            coordsToPixels(pos);

            parameters.Add(new Vector2(Convert.ToSingle(pos[0]), Convert.ToSingle(pos[1])));
            return parameters;
        }

        public void parseLevelData(XElement levelName)
        {
            levelFile = levelName.FirstAttribute?.Value;
            XElement levelData = levelName.Element("LevelData");
            timer = double.Parse(levelData?.Element("Timer")?.Value ?? "-1");
            worldNumber = levelData?.Element("WorldNumber")?.Value.Replace("\"", "");
            levelData?.Remove();
            levelBackground = new Background(0, -35);
            // -35 comes from the two cracked bricks for the ground (16 * 16 = 32)
            // and an extra -3 to make things line up better
        }

        /*private Type[] getParameterTypesArray(int numParams)
        private Vector2 parseVector2Params(XElement e)
        {
            string[] posXY = e.Value.ToString().Split(' ');
            List<object> pos = new List<object>();
            pos.Add(int.Parse(posXY[0]));
            pos.Add(int.Parse(posXY[1]));
            coordsToPixels(pos);

            return new Vector2(Convert.ToSingle(pos[0]), Convert.ToSingle(pos[1]));
        }*/

        private void coordsToPixels(List<object> coordinates)
        {
            int xPixel = (int)coordinates[0] * 64;
            coordinates[0] = xPixel;
            int yPixel = 1000 - (64 * ((int)coordinates[1] + 1));
            coordinates[1] = yPixel;
        }

        public void readXML(string xmlFile)
        {
            levelBackground = null;
            XmlReader r = XmlReader.Create(xmlFile);
            while (r.NodeType != XmlNodeType.Element)
                r.Read();
            XElement e = XElement.Load(r);
            if (e.Name == "Level")
            {
                parseLevelData(e);
                IEnumerable<XElement> afterLevelName = e.Descendants();
                parseXMLTree(e);
            }
            //System.Diagnostics.Debug.WriteLine(e);
        }

        private Type[] getParameterTypesArray(IEnumerable<XElement> childNodes)
        {
            List<Type> parameterTypes = new List<Type>();
            foreach (XElement childNode in childNodes)
            {
                switch (childNode.Name.LocalName.ToLower())
                {
                    case "position":
                        {
                            parameterTypes.Add(typeof(int));
                            parameterTypes.Add(typeof(int));
                            break;
                        }
                    case "items":
                        {
                            parameterTypes.Add(typeof(IGameItems));
                            break;
                        }
                    case "portal":
                    case "levelfile":
                        {
                            parameterTypes.Add(typeof(string));
                            break;
                        }
                    case "rotation":
                        {
                            parameterTypes.Add(typeof(Single));
                            break;
                        }
                    case "outputlocation":
                    case "teleport":
                        {
                            parameterTypes.Add(typeof(Vector2));
                            break;
                        }
                }
            }
            return parameterTypes.ToArray();
        }
    }
}