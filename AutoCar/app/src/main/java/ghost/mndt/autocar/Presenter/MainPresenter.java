package ghost.mndt.autocar.Presenter;

import android.Manifest;
import android.bluetooth.BluetoothAdapter;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Build;
import android.os.IBinder;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.DisplayMetrics;
import android.widget.Toast;

import ghost.mndt.autocar.BlueServer;
import ghost.mndt.autocar.View.BlueListDialog;
import ghost.mndt.autocar.Data;
import ghost.mndt.autocar.View.MainActivity;

/**
 * Created by Ghost on 2018/6/10.
 */

public class MainPresenter {

    private final int REQUEST_ENABLE_BT = 0x001;
    private final int ACCESS_COARSE_LOCATION = 0x002;
    private MainActivity _activity = null;
    private BlueServer _blueServer = null;

    public MainPresenter(final MainActivity activity) {
        _activity = activity;
        Init();
    }

    // 初始化螢幕大小
    private final void Init() {
        final DisplayMetrics displayMetrics = _activity.getResources().getDisplayMetrics();
        Data.WIDTH_PIXELS = displayMetrics.widthPixels;
        Data.HEIGHT_PIXELS = displayMetrics.heightPixels;
    }

    //  綁定藍芽服務, 並開啟藍芽搜尋
    public final void ConnectBlue() {
        _blueServer = new BlueServer();
        final Intent itStart = new Intent(_activity, BlueServer.class);
        _activity.bindService(itStart, _serviceConnection, Context.BIND_AUTO_CREATE);
        OpenBluetooth();
    }

    // 將資料傳送到藍芽裝置
    public final void Write(final String value) {
        if (_blueServer == null || !_blueServer.IsConnect()) {
            Toast.makeText(_activity, "請連接藍芽！", Toast.LENGTH_SHORT).show();
            return;
        }

        _blueServer.Write(value.getBytes());
    }

    // 關閉連接藍芽
    public final void CloseBlue() {
        if(_blueServer == null) {
            Toast.makeText(_activity, "目前無連接藍芽", Toast.LENGTH_SHORT).show();
            return;
        }
        _blueServer.Close();
        _activity.unbindService(_serviceConnection);
        _blueServer = null;
    }

    // 檢查權限和打開藍芽搜尋清單
    private final void OpenBluetooth() {
        final BluetoothAdapter blueAdaConnection = BluetoothAdapter.getDefaultAdapter();
        if (Build.VERSION.SDK_INT >= 23
                && ContextCompat.checkSelfPermission(_activity, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(_activity, new String[]{Manifest.permission.ACCESS_COARSE_LOCATION}, ACCESS_COARSE_LOCATION);
            return;
        }

        if (blueAdaConnection == null) {
            Toast.makeText(_activity.getApplicationContext(), "您的裝置沒有支援藍芽", Toast.LENGTH_SHORT).show();
            return;
        }

        if (!blueAdaConnection.isEnabled()) {
            final Intent enableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
            Toast.makeText(_activity.getApplicationContext(), "請開啟藍芽", Toast.LENGTH_SHORT).show();
            _activity.startActivityForResult(enableIntent, REQUEST_ENABLE_BT);
            return;
        }

        final BlueListDialog blueListDialog = new BlueListDialog(_activity, blueAdaConnection, _blueServer);
        blueListDialog.Show();
    }

    private ServiceConnection _serviceConnection = new ServiceConnection() {

        // 當Service連接事件
        public void onServiceConnected(ComponentName className, IBinder rawBinder) {
            _blueServer = ((BlueServer.LocalBinder) rawBinder).getService();
            _blueServer.InitService();
        }

        // 當Service關閉事件
        public void onServiceDisconnected(ComponentName classname) {
            _blueServer = null;
        }
    };

    // GPS
    public final void LocationChanged(final Location location) {
        if (location != null) {
           _activity.SetLoaction(location.getLatitude(), location.getLongitude());

            if (ActivityCompat.checkSelfPermission(_activity, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED
                    && ActivityCompat.checkSelfPermission(_activity, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                return;
            }
            System.out.println(location.getLatitude() + " " + location.getLongitude());
        }
    }
}
