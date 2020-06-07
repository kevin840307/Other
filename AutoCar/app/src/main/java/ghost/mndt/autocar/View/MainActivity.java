package ghost.mndt.autocar.View;

import android.Manifest;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import ghost.mndt.autocar.Presenter.MainPresenter;
import ghost.mndt.autocar.R;

public class MainActivity extends AppCompatActivity implements LocationListener {
    private MainPresenter _mainPresenter = null;
    private LocationManager _locationManager = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Init();
    }


    private final void Init() {
        _mainPresenter = new MainPresenter(this);

        // 檢查權限
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED
                && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }

        // 初始化GPS並且註冊監聽
        _locationManager = (LocationManager) getSystemService(this.LOCATION_SERVICE);
        _locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 10, 0, this);
        _locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 10, 0, this);
    }

    public final void ConnectBlue(final View view) {
        _mainPresenter.ConnectBlue();
    }

    public final void CloseBlue(final View view) {
        _mainPresenter.CloseBlue();
    }

    // 前進
    public final void Ahead(final View view) {
       _mainPresenter.Write("f");
    }

    // 右轉
    public final void Left(final View view) {
        _mainPresenter.Write("l");
    }

    // 停止
    public final void Stop(final View view) {
        _mainPresenter.Write("s");
    }

    // 左轉
    public final void Right(final View view) {
        _mainPresenter.Write("r");
    }

    // 後退
    public final void Back(final View view) {
        _mainPresenter.Write("b");
    }

    public final void SetLoaction(final double distance, final double angle) {
        final TextView textDistance = (TextView)findViewById(R.id.text_distance);
        final TextView textAngle = (TextView)findViewById(R.id.text_angle);
        textDistance.setText(String.valueOf(distance));
        textAngle.setText(String.valueOf(angle));
    }

    @Override
    public void onLocationChanged(Location location) {
       _mainPresenter.LocationChanged(location);
    }

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {

    }

    @Override
    public void onProviderDisabled(String s) {

    }
}
