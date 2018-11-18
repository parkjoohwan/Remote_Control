﻿//ImageClient.cs

using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;

namespace Remote_Controller
{
    /// <summary>
    /// 이미지 전송 클라이언트
    /// </summary>
    public class ImageClient
    {
        Socket sock;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip">컨트롤러의 IP 주소</param>
        /// <param name="port">컨트롤러의 포트</param>
        public ImageClient(string ip, int port)
        {
            //소켓 생성            
            sock = new Socket(AddressFamily.InterNetwork, //네트워크 주소 체계
                SocketType.Stream,//전송 방식
                ProtocolType.Tcp);//프로토콜

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Connect(ep);//연결 요청
        }

        /// <summary>
        /// 이미지 전송 메서드
        /// </summary>
        /// <param name="img">전송할 이미지</param>
        /// <returns>전송 성공 여부</returns>
        public bool SendImage(Image img)
        {
            if (sock == null)  //소켓이 없을 때
            {
                return false;
            }

            MemoryStream ms = new MemoryStream();//메모리 스트림 개체 생성
            img.Save(ms, ImageFormat.Jpeg);//이미지 개체를 JPEG 포멧으로 메모리 스트림에 저장
            byte[] data = ms.GetBuffer();//메모리 스티림의 버퍼를 가져오기
            try
            {
                int trans = 0;
                byte[] lbuf = BitConverter.GetBytes(data.Length);//버퍼의 크기를 바이트 배열로 변환
                sock.Send(lbuf);//버퍼 길이 전송

                while (trans < data.Length)//전송한 크기가 데이터 길이보다 작으면 반복
                {
                    trans += sock.Send(data, trans, data.Length - trans, SocketFlags.None);//버퍼 전송
                }

                sock.Close();//소켓 닫기
                sock = null;
                return true;
            }
            catch
            {
                Application.Exit();//응용 끝내기
                return false;
            }
        }

        /// <summary>
        /// 비동기로 이미지를 보내는 메서드의 대리자
        /// </summary>
        /// <param name="img">전송할 이미지</param>
        /// <returns>이미지 전송 성공 여부</returns>
        public delegate bool SendImageDele(Image img);//비동기로 이미지를 보내는 메서드의 대리자

        /// <summary>
        /// 이미지를 비동기로 전송하는 메서드
        /// </summary>
        /// <param name="img">전송할 이미지</param>
        /// <param name="callback">이미지 전송을 완료할 때 처리할 콜백</param>
        public void SendImageAsync(Image img, AsyncCallback callback)//이미지를 비동기로 전송
        {
            SendImageDele dele = new SendImageDele(SendImage);//비동기로 이미지 보내는 대리자 생성
            dele.BeginInvoke(img, callback, this);//비동기로 이미지 전송
        }

        /// <summary>
        /// 이미지 클라이언트 닫기 메서드
        /// </summary>
        public void Close()
        {
            if (sock != null)
            {
                sock.Close();//소켓 닫기
                sock = null;
            }
        }
    }
}