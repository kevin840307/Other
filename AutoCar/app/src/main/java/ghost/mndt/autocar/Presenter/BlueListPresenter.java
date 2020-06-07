package ghost.mndt.autocar.Presenter;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import java.lang.reflect.Method;
import java.util.UUID;

import ghost.mndt.autocar.Repository.BlueListRepository;
import ghost.mndt.autocar.View.BlueListDialog;
import ghost.mndt.autocar.Modle.BlueListModle;
import ghost.mndt.autocar.BlueServer;

/**
 * Created by Ghost on 2018/6/10.
 */

public class BlueListPresenter {
    private final UUID MY_UUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
    private BlueListRepository _repository = null;
    private BlueServer _blueServer = null;
    private AppCompatActivity _activity = null;
    private BlueListDialog _blueDialog = null;

    public BlueListPresenter( final BlueListDialog blueDialog, final AppCompatActivity activity, final BlueServer blueServer) {
        _blueDialog = blueDialog;
        _activity = activity;
        _blueServer = blueServer;
        _repository = new BlueListRepository();
    }

    // 添加搜尋到的藍芽
    public final void Add(final BluetoothDevice blueDevice, final int rssi) {
        if (blueDevice == null) {
            return;
        }

        if (blueDevice.getName() == null || blueDevice.getName().length() == 0) {
            return;
        }

        // 若裝置有存在就更新RSSI沒有則新增
        final int index = _repository.GetIndex(blueDevice);
        if (index > -1) {
            _repository.Update(index, rssi);
            return;
        }
        _repository.Add(blueDevice, rssi);
    }

    // 連接藍芽和建立輸出Socket串流
    public final void InitBlueSocket(final BluetoothDevice blueDevice) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    final Method createBondMethod = BluetoothDevice.class.getMethod("createBond");
                    createBondMethod.invoke(blueDevice);

                    final BluetoothSocket blueSocket = blueDevice.createRfcommSocketToServiceRecord(MY_UUID);
                    _blueServer.SetBlueSocket(blueSocket, blueDevice);

                } catch (Exception ex) {
                    Log.e("DeviceStateError", ex.getMessage());
                }
            }
        }).start();
    }

    // 註冊藍芽廣播
    public final void RegisterReceiver() {
        final IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(BluetoothDevice.ACTION_FOUND);
        intentFilter.addAction(BluetoothAdapter.ACTION_DISCOVERY_FINISHED);
        //        intentFilter.addAction(BluetoothDevice.ACTION_UUID);
        //        intentFilter.addAction(BluetoothDevice.EXTRA_UUID);
        //        intentFilter.addAction(BluetoothDevice.EXTRA_DEVICE);
        _activity.registerReceiver(_brReceiver, intentFilter);
    }

    public final void UnregisterReceiver() {

    }

    // 廣播接收, 搜尋到的藍芽裝置
    private BroadcastReceiver _brReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            final String action = intent.getAction();

            if (BluetoothDevice.ACTION_FOUND.equals(action)) {
                final BluetoothDevice device = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                final int RSSI = intent.getShortExtra(BluetoothDevice.EXTRA_RSSI, Short.MIN_VALUE);
                Add(device, RSSI);
                _blueDialog.RefreshListView(_repository.GetBlueModles());
            }

        }
    };
}
