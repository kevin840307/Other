package ghost.mndt.autocar.View;

import android.app.Dialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;

import ghost.mndt.autocar.BlueListAdapter;
import ghost.mndt.autocar.BlueServer;
import ghost.mndt.autocar.Data;
import ghost.mndt.autocar.Modle.BlueListModle;
import ghost.mndt.autocar.Presenter.BlueListPresenter;
import ghost.mndt.autocar.R;

/**
 * Created by Ghost on 2018/6/10.
 */

public class BlueListDialog {
    private String TAG = "BlueListDialog";
    private Dialog _blueDialog = null;
    private AppCompatActivity _activity = null;
    private BlueListPresenter _presenter = null;
    private BluetoothAdapter _blueAdapter = null;

    public BlueListDialog(final AppCompatActivity activity, final BluetoothAdapter blueAdapter, final BlueServer blueServer) {
        _activity = activity;
        _blueAdapter = blueAdapter;
        _presenter = new BlueListPresenter(this, activity, blueServer);
        Init();
    }


    public final void Show() {
        _blueDialog.show();
    }

    private final void Init() {
        BlueSearch();
        fnInitUIControl();
    }

    private final void fnInitUIControl() {
        InitDiaLog();
    }

    private final void BlueSearch() {
        _presenter.RegisterReceiver();
        _blueAdapter.startDiscovery();
    }

    private final void InitDiaLog() {
        _blueDialog = new Dialog(_activity, R.style.Dialog);
        _blueDialog.setContentView(R.layout.blue_list_show);

        final Window dialogWindow = _blueDialog.getWindow();
        final WindowManager.LayoutParams layParams = dialogWindow.getAttributes();

        dialogWindow.setGravity(Gravity.CENTER);
        layParams.width = (int) ((double) Data.WIDTH_PIXELS / 1.1);
        layParams.height = (int) ((double) Data.HEIGHT_PIXELS / 1.1);
        layParams.alpha = 0.8f;
    }

    public final void RefreshListView(final ArrayList<BlueListModle> blueDevice) {
        final ListView lvBlue = (ListView) _blueDialog.findViewById(R.id.lv_blue_show);
        final BlueListAdapter adapter = new BlueListAdapter(_activity, blueDevice);

        lvBlue.setAdapter(adapter);
        lvBlue.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Toast.makeText(_activity, "你選擇" + ((BluetoothDevice)adapter.getItem(position)).getName(), Toast.LENGTH_SHORT).show();
                _presenter.UnregisterReceiver();
                _presenter.InitBlueSocket((BluetoothDevice)adapter.getItem(position));
                _blueDialog.cancel();
            }
        });
    }
}
