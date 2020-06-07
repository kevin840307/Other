package ghost.mndt.autocar.Modle;

import android.bluetooth.BluetoothDevice;

import java.util.ArrayList;

/**
 * Created by Ghost on 2017/4/21.
 */
public class BlueListModle {
    private BluetoothDevice _bluedevice = null;
    private int _rssi = 0;

    public BlueListModle(final BluetoothDevice blueDevice, final int rssi) {
        _bluedevice = blueDevice;
        _rssi = rssi;
    }

    public final BluetoothDevice GetBlueDevice() {
       return _bluedevice;
    }

    public final int GetRssi() {
       return _rssi;
    }

    public final void SetRssi(final int rssi) {
        _rssi = rssi;
    }
}
