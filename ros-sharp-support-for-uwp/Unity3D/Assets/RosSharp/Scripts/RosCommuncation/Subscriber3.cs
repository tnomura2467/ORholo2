/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]



    public class Subscriber3 : MonoBehaviour
    {

        public string Topic;
        public string Topic2;
        public string Topic3;
        public string Topic4;
        public string Topic5;
        IDdecision iddecision;
        GameObject WorldEditorID;

        private int holoid;

        public float TimeStep;
        private int timeStep { get { return (int)(TimeStep * 1000); } } // the rate(in ms in between messages) at which to throttle the topics

        public MessageReceiver MessageReceiver1;
        public MessageReceiver MessageReceiver2;
        public MessageReceiver MessageReceiver3;
        public MessageReceiver MessageReceiver4;
        public MessageReceiver MessageReceiver5;

        private RosSocket rosSocket;
        private RosSocket rosSocket2;
        private RosSocket rosSocket3;
        private RosSocket rosSocket4;
        private RosSocket rosSocket5;

        private void Start()
        {
            WorldEditorID = GameObject.Find("WorldEditor");
            iddecision = WorldEditorID.GetComponent<IDdecision>();

            holoid = iddecision.ids;

            Topic = Topic + holoid.ToString("0") + "/compressed";
            Topic2 = Topic2 + holoid.ToString("0") + "/compressed";
            Topic3 = Topic3 + holoid.ToString("0") + "/compressed";
            Topic4 = Topic4 + holoid.ToString("0") + "/compressed";
            Topic5 = Topic5 + holoid.ToString("0") + "/compressed";

            rosSocket = GetComponent<RosConnector>().RosSocket;
            rosSocket2 = GetComponent<RosConnector>().RosSocket;
            rosSocket3 = GetComponent<RosConnector>().RosSocket;
            rosSocket4 = GetComponent<RosConnector>().RosSocket;
            rosSocket5 = GetComponent<RosConnector>().RosSocket;

            rosSocket2.Subscribe(Topic2, MessageTypes.RosMessageType(MessageReceiver2.MessageType), Receive2, timeStep);
            rosSocket.Subscribe(Topic, MessageTypes.RosMessageType(MessageReceiver1.MessageType), Receive1, timeStep);
            rosSocket3.Subscribe(Topic3, MessageTypes.RosMessageType(MessageReceiver3.MessageType), Receive3, timeStep);
            rosSocket4.Subscribe(Topic4, MessageTypes.RosMessageType(MessageReceiver4.MessageType), Receive4, timeStep);
            rosSocket5.Subscribe(Topic5, MessageTypes.RosMessageType(MessageReceiver5.MessageType), Receive5, timeStep);
        }

        private void Receive2(Message message)
        {
            MessageReceiver2.RaiseMessageReception(new MessageEventArgs(message));
        }

        private void Receive1(Message message)
        {
            MessageReceiver1.RaiseMessageReception(new MessageEventArgs(message));
        }

        

        private void Receive3(Message message)
        {
            MessageReceiver3.RaiseMessageReception(new MessageEventArgs(message));
        }

        private void Receive4(Message message)
        {
            MessageReceiver4.RaiseMessageReception(new MessageEventArgs(message));
        }



        private void Receive5(Message message)
        {
            MessageReceiver5.RaiseMessageReception(new MessageEventArgs(message));
        }
    }
}