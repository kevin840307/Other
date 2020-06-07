package ghost.mndt.autocar;

import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

import ghost.mndt.autocar.Modle.BlueListModle;

/**
 * Created by Ghost on 2018/6/10.
 */

public class BlueListAdapter extends BaseAdapter {

    private ArrayList<BlueListModle>  _blueDevice = null;
    private LayoutInflater _layoutInflater = null;

    public BlueListAdapter(final Context context, final ArrayList<BlueListModle> blueDevice) {
        _blueDevice = blueDevice;
        _layoutInflater = LayoutInflater.from(context);
    }
    @Override
    public int getCount() {
        return _blueDevice.size();
    }

    @Override
    public Object getItem(int i) {
        return _blueDevice.get(i).GetBlueDevice();
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        view = _layoutInflater.inflate(R.layout.list_item_style1, null);

        final TextView textRis = (TextView) view.findViewById(R.id.text_ris);
        final TextView textName = (TextView) view.findViewById(R.id.text_name);
        final TextView textMac = (TextView) view.findViewById(R.id.text_mac);

        textRis.setText(String.valueOf(_blueDevice.get(i).GetRssi()));
        textName.setText(_blueDevice.get(i).GetBlueDevice().getName());
        textMac.setText(_blueDevice.get(i).GetBlueDevice().getAddress());

        return view;
    }
}
