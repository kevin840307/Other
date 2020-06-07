package ghost.mndt.autocar;

import android.annotation.TargetApi;
import android.app.Service;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGatt;
import android.bluetooth.BluetoothGattCallback;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothGattDescriptor;
import android.bluetooth.BluetoothGattService;
import android.bluetooth.BluetoothManager;
import android.bluetooth.BluetoothProfile;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.content.Intent;
import android.os.Binder;
import android.os.Build;
import android.os.IBinder;
import android.os.ParcelUuid;
import android.support.annotation.Nullable;
import android.util.Log;

import java.io.IOException;
import java.io.OutputStream;
import java.util.UUID;

/**
 * Created by Ghost on 2018/6/10.
 */
@TargetApi(Build.VERSION_CODES.JELLY_BEAN_MR2)
public class BlueServer extends Service {
    private final String TAG = "BlueServer";
    private final IBinder _IBinder = new LocalBinder();
    private final UUID RX_SERVICE_UUID = UUID.fromString("0000ffe0-0000-1000-8000-00805f9b34fb");
    private final UUID RX_CHAR_UUID = UUID.fromString("0000ffe1-0000-1000-8000-00805f9b34fb");
    private BluetoothManager _blueManager = null;
    private BluetoothAdapter _blueAdapter = null;
    private BluetoothSocket _blueSocket = null;
    private BluetoothDevice _blueDevice = null;
    private BluetoothGatt _blueGatt = null;
    private OutputStream _outStream = null;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return _IBinder;
    }

    // 初始化藍芽服務
    public final boolean InitService() {

        _blueManager = (BluetoothManager) getSystemService(Context.BLUETOOTH_SERVICE);

        if (_blueManager == null) {
            Log.e(TAG, "BluetoothManager Init error");
            return false;
        }

        _blueAdapter = _blueManager.getAdapter();

        if (_blueAdapter == null) {
            Log.e(TAG, "BluetoothAdapter Init error.");
            return false;
        }
        return true;
    }

    // 連接藍芽
    public final void SetBlueSocket(final BluetoothSocket blueSocket, final BluetoothDevice blueDevice) {
        _blueSocket = blueSocket;
        _blueDevice = blueDevice;
        fnConnect();
        try {
            _outStream = _blueSocket.getOutputStream();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private final boolean fnConnect() {
        _blueGatt = _blueDevice.connectGatt(this, false, _bluecallGattCallback);
        return true;
    }

    public final void Close() {
        Write("s".getBytes());
        _blueGatt.close();
    }

    public final boolean IsConnect() {
        return !(_outStream == null);
    }

    // 將資料寫入藍芽的服務UUID
    public final void Write(final byte[] value) {
        final BluetoothGattService blueGettService = _blueGatt.getService(RX_SERVICE_UUID);

        if (blueGettService == null) {
            return;
        }
        final BluetoothGattCharacteristic biueGattChar = blueGettService.getCharacteristic(RX_CHAR_UUID);

        if (biueGattChar == null) {
            return;
        }

        biueGattChar.setValue(value);
        _blueGatt.writeCharacteristic(biueGattChar);
        _blueGatt.readRemoteRssi();
    }

    public class LocalBinder extends Binder {
        public final BlueServer getService() {
            return BlueServer.this;
        }
    }

    private final BluetoothGattCallback _bluecallGattCallback = new BluetoothGattCallback() {

        //override callback functions
        @Override
        public void onCharacteristicChanged(BluetoothGatt gatt,
                                            BluetoothGattCharacteristic characteristic) {
            byte[] value = characteristic.getValue();
            for (int iPos = 0; iPos < value.length; iPos++) {
                Log.i(TAG, String.valueOf(value[iPos]));
            }

        }

        @Override
        public void onCharacteristicRead(BluetoothGatt gatt,
                                         BluetoothGattCharacteristic characteristic, int status) {
        }

        @Override
        public void onCharacteristicWrite(BluetoothGatt gatt,
                                          BluetoothGattCharacteristic characteristic, int status) {
            super.onCharacteristicWrite(gatt, characteristic, status);
            if (status == BluetoothGatt.GATT_SUCCESS) {
                //byte[] value = characteristic.getValue();
                //Log.i(TAG, "發送" + value);
            }
        }

        @Override
        public void onConnectionStateChange(BluetoothGatt gatt, int status, int newState) {
            if (newState == BluetoothProfile.STATE_CONNECTED) {
                Log.i(TAG, "Connected to GATT server1.");
                _blueGatt.discoverServices();
                _blueGatt.connect();
            } else if (newState == BluetoothProfile.STATE_DISCONNECTED) {
                Log.i(TAG, "Connected to GATT server2.");
                _blueGatt.discoverServices();
                _blueGatt.connect();
            }
        }

        @Override
        public void onDescriptorRead(BluetoothGatt gatt,
                                     BluetoothGattDescriptor descriptor, int status) {
        }

        @Override
        public void onDescriptorWrite(BluetoothGatt gatt,
                                      BluetoothGattDescriptor descriptor, int status) {
        }

        private static final double A_Value = 50;/* 1米信號強度*/
        private static final double n_Value = 2.5;/*環境衰減因素*/

        @Override
        public void onReadRemoteRssi(BluetoothGatt gatt, int rssi, int status) {
            int iRssi = Math.abs(rssi);
            double power = (iRssi - A_Value) / (10 * n_Value);

            Log.i(TAG, "rssi" + rssi);
            Log.i(TAG, "rssi距離數值" + Math.pow(10, power));
        }

        @Override
        public void onReliableWriteCompleted(BluetoothGatt gatt, int status) {
        }

        @Override
        public void onServicesDiscovered(BluetoothGatt gatt, int status) {
            super.onServicesDiscovered(gatt, status);
            if (status == BluetoothGatt.GATT_SUCCESS) {
                Log.i(TAG, "已開始監聽");
                for (BluetoothGattService gattService : gatt.getServices()) {
                    Log.i(TAG, "Service UUID Found: " + gattService.getUuid().toString());
                }
            }
        }
    };
}
