import HeaderComponent from "widgets/Header/HeaderComponent";
import ChannelListUI from "widgets/Channel/ChannelListUI";
import NewsListUI from "widgets/News/NewsListUI";
import styles from './MainPage.module.css';
import ModalInfoUI from "widgets/Modals/ModalInfo/ModalInfoUI";
import type { RootState } from 'app/store';
import { useSelector } from 'react-redux';
import SpinnerUI from "widgets/Spinner/SpinnerUI";

const MainPage = () => {

  const info = useSelector((state: RootState) => state.info);
  
  return (
    <div className={styles.page}>
      <HeaderComponent />
      <div className={styles.container}>
        <ChannelListUI />
        <NewsListUI />
      </div>
      <ModalInfoUI isOpen={info.isVisible} description={info.text} isSuccess={info.isSuccess} />
      <SpinnerUI />
    </div>
  );
}

export default MainPage;