using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

public class FirebaseDatabaseController : MonoBehaviour
{
    static public FirebaseDatabaseController instance;

   /* #region singleton
    void Awake() // 객체 생성 시 최초 실행
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion*/

    //Json 파일로 만들기 위해 class 정의
    public class User
    {
        public string userId;
        public string email;
        public float clearTime;
        public User(string userId,string email,float clearTime)
        {
            this.userId = userId;
            this.email = email;
            this.clearTime = clearTime;
          
        }
    }

    [SerializeField]
    private GameObject RankingPanel;

    [SerializeField]
    private GameObject RankingContent;

    [SerializeField]
    private GameObject RankingView;

    //DatabaseRefernce 변수 선언
    static DatabaseReference reference;

    List<User> rankingList = new List<User>();

    Dictionary<int, User> rankingdic = new Dictionary<int, User>();

    bool One_Frame = true;


    /*public Text[] Rank = new Text[7];

    private string[] strRank;
    private long strLen;

    private bool textLoadBool = false;*/



    void Start()
    {
        //데이터를 쓰려면 DataReference의 인스턴스가 필요.
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://sgptest-6f3a0-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        rankingList.Clear();
    }

    void Update()
    {
        //현재 첫번째가 Text UI가 "Loading" 이면,
        //즉, 스크립트를 컴포넌트하고 있는 게임 오브젝트가 Activeself(true)이면,
            DataLoad();
      
    }

    /* void LateUpdate()
     {
         if (textLoadBool)
         {
             TextLoad();
         }
         if (Time.timeScale != 0.0f) Time.timeScale = 0.0f;
     }*/

     void DataLoad()
     {
           
        Debug.Log("데이터 불러오는중");

        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        // reference의 자식(userId)를 task로 받음
        reference.OrderByChild("clearTime").GetValueAsync().ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 Debug.Log("데이터베이스 불러오기가 취소되었습니다");
                 return;
             }
             if (task.IsFaulted)
             {
                 Debug.Log("데이터베이스 불러오기 중 오류가 발생했습니다");
                 return;
             }

             //task가 성공적이면
             else if (task.IsCompleted)
             {
                 //DataSnapshot 변수를 선언하여 task의 결과 값을 받음
                 DataSnapshot snapshot = task.Result;

                 //snapshot의 자식 개수를 확인
                 Debug.Log(snapshot.ChildrenCount);

                 int a = 0;

                 //foreach문으로 각각 데이터를 IDictionary로 변환해 각 이름에 맞게 변수 초기화
                 foreach (DataSnapshot data in snapshot.Children)
                 {
                     string userId = data.Key;
                     string email = data.Child("email").Value.ToString();
                     float clearTime = (float)Math.Round(float.Parse(data.Child("clearTime").Value.ToString()), 2);

                     User user = new User(userId, email, clearTime);
                     rankingdic.Add(a, user);
                     ++a;
                 }

                 rankingdic = rankingdic.OrderBy(data => data.Value.clearTime).ToDictionary(x => x.Key, x => x.Value);


                 for (int i = 0; i < rankingdic.Count; i++)
                 {
                     Debug.Log("랭킹 " + (i + 1) + ": " + rankingdic.Skip(i).First().Value.email + ", 점수: " + rankingdic.Skip(i).First().Value.clearTime);

                 }



             }


           

         });



    }

    /* void TextLoad()
     {
         textLoadBool = false;
         try
         {
             //받아온 데이터 정렬 = > 위에서부터 아래로
             Array.Sort(strRank, (x, y) => string.Compare
              (y.Substring(y.Length - 5, 5).ToString() + x.Substring(x.Length - 5, 5).ToString(),
               x.Substring(x.Length - 5, 5).ToString() + y.Substring(y.Length - 5, 5).ToString()));
         }
         catch(NullReferenceException e)
         {
             Console.WriteLine(e);
             return;
         }

         for(int i = 0; i < Rank.Length; i++)
         {
             //Text UI에 현재 가지고 있는 str 길이 까지만 보여주기 위함.
             if (strLen <= i) return;
             Rank[i].text = strRank[i];
         }
     }*/

    //저장 버튼 클릭 시
    static public void OnSave()
    {
        // writeNewUser 함수로 userId,username,email 정의
        writeNewUser(FirebaseAuthManager.Instance.userId,FirebaseAuthManager.Instance.userEmail,RankingTimer.instance.GetTime() );
    }



    static private void writeNewUser(string userId,string email,float clear_time)
    {
        // 클래스 User 변수를 만들고 받아온 name, email을 대입.
        User user = new User(userId,email,clear_time);
        // 대입 시킨 클래스 변수 user를 json 파일로 변환
        string json = JsonUtility.ToJson(user);
        //DatabaseReference 변수에 userId를 자식으로 변환된 json 파일을 업로드
        reference.Child(userId).SetRawJsonValueAsync(json);
    }



    public void readUser()
    { 
            RankingView.SetActive(true);

            for (int i = 0; i < rankingdic.Count; i++)
            {
                GameObject ranking = Instantiate(RankingPanel);
                ranking.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + (i + 1);
                ranking.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankingdic.Skip(i).First().Value.email;
                ranking.transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + rankingdic.Skip(i).First().Value.clearTime + "초";
                ranking.transform.SetParent(RankingContent.transform);
            }

        
    }
  
}
