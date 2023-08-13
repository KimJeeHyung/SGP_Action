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
    void Awake() // ��ü ���� �� ���� ����
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

    //Json ���Ϸ� ����� ���� class ����
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

    //DatabaseRefernce ���� ����
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
        //�����͸� ������ DataReference�� �ν��Ͻ��� �ʿ�.
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://sgptest-6f3a0-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        rankingList.Clear();
    }

    void Update()
    {
        //���� ù��°�� Text UI�� "Loading" �̸�,
        //��, ��ũ��Ʈ�� ������Ʈ�ϰ� �ִ� ���� ������Ʈ�� Activeself(true)�̸�,
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
           
        Debug.Log("������ �ҷ�������");

        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        // reference�� �ڽ�(userId)�� task�� ����
        reference.OrderByChild("clearTime").GetValueAsync().ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 Debug.Log("�����ͺ��̽� �ҷ����Ⱑ ��ҵǾ����ϴ�");
                 return;
             }
             if (task.IsFaulted)
             {
                 Debug.Log("�����ͺ��̽� �ҷ����� �� ������ �߻��߽��ϴ�");
                 return;
             }

             //task�� �������̸�
             else if (task.IsCompleted)
             {
                 //DataSnapshot ������ �����Ͽ� task�� ��� ���� ����
                 DataSnapshot snapshot = task.Result;

                 //snapshot�� �ڽ� ������ Ȯ��
                 Debug.Log(snapshot.ChildrenCount);

                 int a = 0;

                 //foreach������ ���� �����͸� IDictionary�� ��ȯ�� �� �̸��� �°� ���� �ʱ�ȭ
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
                     Debug.Log("��ŷ " + (i + 1) + ": " + rankingdic.Skip(i).First().Value.email + ", ����: " + rankingdic.Skip(i).First().Value.clearTime);

                 }



             }


           

         });



    }

    /* void TextLoad()
     {
         textLoadBool = false;
         try
         {
             //�޾ƿ� ������ ���� = > ���������� �Ʒ���
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
             //Text UI�� ���� ������ �ִ� str ���� ������ �����ֱ� ����.
             if (strLen <= i) return;
             Rank[i].text = strRank[i];
         }
     }*/

    //���� ��ư Ŭ�� ��
    static public void OnSave()
    {
        // writeNewUser �Լ��� userId,username,email ����
        writeNewUser(FirebaseAuthManager.Instance.userId,FirebaseAuthManager.Instance.userEmail,RankingTimer.instance.GetTime() );
    }



    static private void writeNewUser(string userId,string email,float clear_time)
    {
        // Ŭ���� User ������ ����� �޾ƿ� name, email�� ����.
        User user = new User(userId,email,clear_time);
        // ���� ��Ų Ŭ���� ���� user�� json ���Ϸ� ��ȯ
        string json = JsonUtility.ToJson(user);
        //DatabaseReference ������ userId�� �ڽ����� ��ȯ�� json ������ ���ε�
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
                ranking.transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + rankingdic.Skip(i).First().Value.clearTime + "��";
                ranking.transform.SetParent(RankingContent.transform);
            }

        
    }
  
}
