package ghost.mndt.autocar.Repository;

import android.bluetooth.BluetoothDevice;

import java.util.ArrayList;

import ghost.mndt.autocar.Modle.BlueListModle;

/**
 * Created by Ghost on 2018/6/11.
 */

public class BlueListRepository {
    private ArrayList<BlueListModle> _blDevices = null;
    private ArrayList<String> _blMacs = null;

    public BlueListRepository() {
        _blDevices = new ArrayList();
        _blMacs = new ArrayList();
    }

    // 添加藍芽裝置
    public final void Add(final BluetoothDevice blueDevice, final int rssi) {
        _blDevices.add(new BlueListModle(blueDevice, rssi));
        _blMacs.add(blueDevice.getAddress());
    }

    // 更新藍芽rssi
    public final void Update(final int index, final int rssi) {
        _blDevices.get(index).SetRssi(rssi);
    }

    public final int GetIndex(final BluetoothDevice blueDevice) {
        return _blMacs.indexOf(blueDevice.getAddress());
    }

    public final ArrayList<BlueListModle> GetBlueModles() {
        return _blDevices;
    }

}
