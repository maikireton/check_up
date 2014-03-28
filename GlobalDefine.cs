using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace check_up02
{
    struct TJ_YUYUE
    {
        public int mID;
        public int mPeopleID;
        public int mSuitID;
        public DateTime mDate;
    }

    struct TJ_RESULT
    {
        public int mID;
        public int mPeopleID;
        public Dictionary<int, string> mXiangMuResult;//项目表的id->result 用于更新
        public List<string> mListResult;//结果
    }

    struct TJ_KESHI
    {
        public int mID;
        public string mName;
    }

    struct TJ_XIANGMU
    {
        public int mID;
        public string mName;
        public string mType;
    }

    struct TJ_PEOPLE
    {
        public int mID;
        public string mName;
        public string mSex;
        public int mAge;
        public string mTel;
        public string mCorporation;
        public string mAddress;
    }

    struct TJ_SUIT
    {
        public int mID;
        public string mName;
        public string mDate;
        public string mSuitString;//从小到大1,2,3
        public List<TJ_XIANGMU> mListXiangMu;
    }

    struct TJ_SUIT_NEIKE
    {
        public int mID;
        public int mSuitID;
        public bool mXueYa;
    }

    struct TJ_SUIT_WAIKE
    {
        public int mID;
        public int mSuitID;
        public bool mHeight;
    }
}
