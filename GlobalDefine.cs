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
        public TJ_SUIT_WAIKE mWaiKe;
        public TJ_SUIT_NEIKE mNeiKe;
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

    struct TJ_RESULT
    {
        public int mID;
        public int mPeopleID;
        public int mSuitID;
        public DateTime mDate;
        public TJ_RESULT_WAIKE mWaiKe;
        public TJ_RESULT_NEIKE mNeiKe;
    }

    struct TJ_RESULT_NEIKE
    {
        public int mID;
        public int mResultID;
        public string mXueYa;
        public string mXinLv;
    }

    struct TJ_RESULT_WAIKE
    {
        public int mID;
        public int mResultID;
        public int mHeight;
    }
}
