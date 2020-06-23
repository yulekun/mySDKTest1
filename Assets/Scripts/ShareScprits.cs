using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine;
using UnityEngine.UI;
public class ShareScprits : MonoBehaviour {
	public ShareSDK ssdk;
	public Text mText;
	public Text mText2;
	// Use this for initialization
	ShareContent content;
	void Start () {
		if (mText != null) {
			mText.text = "111";
		}

		ssdk.shareHandler = ShareResultHandler;
		ssdk.authHandler = AuthResultHandler;
		ssdk.showUserHandler = GetUserInfoResultHandler;
		content = new ShareContent ();
		content.SetText ("测试分享");
		content.SetImageUrl ("http://txzqcdn.labi.qq.com/Intest/share/ShareInvite0701_Cue.jpg");
		content.SetTitle ("分享");
		content.SetShareType (ContentType.Image);
	}

	// Update is called once per frame
	void Update () {

	}

	public void shareBtnByQQ () {
		if (ssdk) {
			if (!ssdk.IsAuthorized (PlatformType.QQ)) {
				ssdk.Authorize (PlatformType.QQ);
				return;
			}
			ssdk.ShareContent (PlatformType.QQ, content);
		}
	}

	public void shareBtnByWX () {
		if (ssdk) {
			if (!ssdk.IsAuthorized (PlatformType.WeChat)) {
				ssdk.Authorize (PlatformType.WeChat);
				return;
			}
			ssdk.ShareContent (PlatformType.WeChat, content);
		}
	}

	public void shareByList () {
		ssdk.ShowPlatformList (null, content, 100, 100);
	}

	public void GetQQUserInfo () {
		ssdk.GetUserInfo (PlatformType.QQ);
	}

	public void GetWXUSerInfo () {
		ssdk.GetUserInfo (PlatformType.WeChat);
	}

	private void ShareResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable data) {
		if (state == ResponseState.Success) {
			if (mText != null) {
				mText.text = "分享成功";
			}
		} else if (state == ResponseState.Fail) {
			if (mText != null) {
				mText.text = "分享失败";
			}
		} else if (state == ResponseState.Cancel) {
			if (mText != null) {
				mText.text = "分享取消";
			}
		}

	}

	private void AuthResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result) {
		if (state == ResponseState.Success) {
			if (mText2 != null) {
				mText2.text = "授权成功";
			}
		} else if (state == ResponseState.Fail) {
			if (mText2 != null) {
				mText2.text = "授权失败";
			}
		} else if (state == ResponseState.Cancel) {
			if (mText2 != null) {
				mText2.text = "授权取消";
			}
		}
	}

	void GetUserInfoResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result) {
		if (state == ResponseState.Success) {
			if (mText2 != null) {
				mText2.text = MiniJSON.jsonEncode (result);
			}
		} else if (state == ResponseState.Fail) {
			if (mText2 != null) {
				mText2.text = "获取信息失败：" + result["error_code"] + "msg:" + result["error_msg"];
			}
		} else if (state == ResponseState.Cancel) {
			if (mText2 != null) {
				mText2.text = "获取信息失败：Cancel";
			}
		}
	}
}